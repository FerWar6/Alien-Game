using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable_BreakerSwitch : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "E to flip breaker";
    [SerializeField] AudioClip activationClip;

    public void Interact()
    {
        AudioManager.instance.SetAudioClip(activationClip, transform.position, 1, true);
        PlayerData.instance.OnBreakerActive.Invoke();
        Destroy(this);
    }
}
