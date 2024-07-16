using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == PlayerData.instance.playerPos.gameObject.name)
        {
            ToggleAnim();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == PlayerData.instance.playerPos.gameObject.name)
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
}
