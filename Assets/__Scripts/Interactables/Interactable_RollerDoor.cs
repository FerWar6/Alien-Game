using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_RollerDoor : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    private string message = "E to test door";

    private Animator anim;
    bool isClosed = true;

    private void Update()
    {
        anim = GetComponent<Animator>();
    }
    public void Interact()
    {
        if (isClosed)
        {
            anim.SetTrigger("open");
            //anim.ResetTrigger("close");
            isClosed = false;
        }
        if (!isClosed)
        {
            anim.SetTrigger("close");
            anim.ResetTrigger("open"); 
            isClosed = true;
        }
    }
}
