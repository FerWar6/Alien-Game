using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.enabled = true;
            anim.SetTrigger("StartTest");
        }
    }
    public void _StopAnim()
    {
        anim.enabled = false;
    }
}
