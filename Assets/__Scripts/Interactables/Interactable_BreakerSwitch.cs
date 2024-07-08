using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable_BreakerSwitch : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "Fix breakers to turn on power";
    [SerializeField] Puzzle_RowManager sliderMan;
    [SerializeField] AudioClip activationClip;

    private bool CanUseBreaker = false;
    private void Start()
    {
        if (sliderMan != null) sliderMan.OnPuzzleCompleted.AddListener(EnableBreaker);
    }
    public void Interact()
    {
        if (CanUseBreaker)
        {
            AudioManager.instance.SetAudioClip(activationClip, transform.position, 1, true);
            PlayerData.instance.OnBreakerActive.Invoke();
            GetComponent<Animator>().SetTrigger("FlipBreaker");
            Destroy(this);
        }
    }
    private void EnableBreaker()
    {
        CanUseBreaker = true;
        message = "E to turn on power";
    }
    public void SetMessage(string text)
    {
        message = text;
    }
    private void OnDestroy()
    {
        if (sliderMan != null) sliderMan.OnPuzzleCompleted.RemoveListener(EnableBreaker);
    }
}
