using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBreakerLampManager : MonoBehaviour
{
    [SerializeField] private Material nonEmissiveMat;
    [SerializeField] private Material emissiveMat;
    public void SetLight(bool on)
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Light light = GetComponentInChildren<Light>();
        if (on)
        {
            mesh.material = emissiveMat;
            light.intensity = 0.75f;
        }
        else
        {
            mesh.material = nonEmissiveMat;
            light.intensity = 0.2f;
        }
    }
}
