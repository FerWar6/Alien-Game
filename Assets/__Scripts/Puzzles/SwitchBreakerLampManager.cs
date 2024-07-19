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
            light.type = LightType.Spot;
            mesh.material = emissiveMat;
            light.intensity = 0.9f;
        }
        else
        {
            light.type = LightType.Point;
            mesh.material = nonEmissiveMat;
            light.intensity = 0.2f;
        }
    }
}
