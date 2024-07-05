using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    public Transform UICamPos;
    private void Start()
    {
        UICamPos = GetComponentInChildren<UICamPos>().transform;
    }
}
