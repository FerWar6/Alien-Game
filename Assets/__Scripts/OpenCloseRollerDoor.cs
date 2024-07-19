using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseRollerDoor : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private Interactable_Terminal interTer; //interactable Terminal

    private void Start()
    {
        if(interTer != null) interTer.OnTerminalStatusChange.AddListener(OpenCloseDoor);
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
            Destroy(this);
        }
        else
        {
            ToggleAnim();
        }
    }
    void ToggleAnim()
    {
        if (anim.GetInteger("DoorState") == 0 || anim.GetInteger("DoorState") == 2)
        {
            anim.SetInteger("DoorState", 1);
        }
        else
        {
            anim.SetInteger("DoorState", 2);
        }
    }
    private void OnDestroy()
    {
        interTer.OnTerminalStatusChange.RemoveListener(OpenCloseDoor);
    }
}
