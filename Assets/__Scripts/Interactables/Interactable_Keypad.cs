using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Keypad : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "E to turn off alarm";

    public void Interact()
    {
        PlayerData.instance.OnAlarmDeactivate.Invoke();
    }
}
