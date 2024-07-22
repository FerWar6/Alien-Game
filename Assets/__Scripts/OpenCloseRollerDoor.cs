using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseRollerDoor : MonoBehaviour
{

    private Animator anim;
    private AudioSource auSource;
    private bool needsToBeDestroyed = false;
    [SerializeField] private Interactable_Terminal interTer; //interactable Terminal
    [SerializeField] private AudioClip doorOpenClip;
    [SerializeField] private AudioClip doorStopClip;
    private void Start()
    {
        anim = GetComponent<Animator>();
        auSource = GetComponent<AudioSource>();
        if (interTer != null) interTer.OnTerminalStatusChange.AddListener(OpenCloseDoor);
        else
        {
            Debug.Log("rollerdoor failed to link to interactive terminal script");
        }
    }
    private void OpenCloseDoor(bool lastTime)
    {
        if (lastTime)
        {
            anim.SetInteger("DoorState", 2);
            needsToBeDestroyed = true;
        }
        else
        {
            ToggleAnim();
        }
    }
    private void OnOpenDoor()
    {
        if (anim.GetInteger("DoorState") == 1)
        {
            auSource.Stop();
            auSource.clip = doorStopClip;
            auSource.Play();
        }
    }
    private void OnCloseDoor()
    {
        if (anim.GetInteger("DoorState") == 2)
        {
            auSource.Stop();
            auSource.clip = doorStopClip;
            auSource.Play();
        }
    }
    private void DestroyCheck()
    {
        if (needsToBeDestroyed)
        {
            Destroy(this);
        }
    }
    void ToggleAnim()
    {
        if (anim.GetInteger("DoorState") == 0 || anim.GetInteger("DoorState") == 2)
        {
            anim.SetInteger("DoorState", 1);
            auSource.clip = doorOpenClip;
            auSource.Play();
        }
        else
        {
            anim.SetInteger("DoorState", 2);
            auSource.clip = doorOpenClip;
            auSource.Play();
        }
    }
    private void OnDestroy()
    {
        interTer.OnTerminalStatusChange.RemoveListener(OpenCloseDoor);
    }
}
