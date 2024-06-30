using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPuzzleManager : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "E to start puzzle";
    [SerializeField] private float speed = 5f;

    [SerializeField] private Transform endPos;


    private void Update()
    {
        message = PlayerData.instance.inUI ? null : "E to start puzzle";
    }
    public void Interact()
    {
        if(!PlayerData.instance.inUI)
        {
            PlayerData.instance.OnEnterUI.Invoke(endPos);
        }
    }
   }
