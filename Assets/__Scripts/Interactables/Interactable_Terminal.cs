using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Terminal : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "E to start puzzle";

    private Transform UICamPos;


    private void Update()
    {
        UICamPos = GetComponentInChildren<UICamPos>().transform;
        message = PlayerData.instance.inUI ? null : "E to start puzzle";
    }
    public void Interact()
    {
        if (!PlayerData.instance.inUI)
        {
            PlayerData.instance.OnEnterUI.Invoke(UICamPos);
        }
    }
}
