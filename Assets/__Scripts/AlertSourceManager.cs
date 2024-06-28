using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSourceManager : MonoBehaviour
{
    public float cooldown;
    public float timeExisted;

    private void FixedUpdate()
    {
        cooldown -= Time.deltaTime;
        timeExisted += Time.deltaTime;
    }
    private void OnDisable()
    {
        cooldown = 0;
        timeExisted = 0;
    }
}
