using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipLightsManager : MonoBehaviour
{
    [SerializeField] Material shipLightMaterial;
    void Start()
    {
        PlayerData.instance.OnBreakerActive.AddListener(TurnOnLights);
        shipLightMaterial.DisableKeyword("_EMISSION");
    }

    // The function to be called by the UnityEvent
    public void TurnOnLights()
    {
        shipLightMaterial.EnableKeyword("_EMISSION");
        // Add all found Light components to the alarmLightSources list
        Light[] descendantLights = GetComponentsInChildren<Light>();
        List<Light> shipLightSources = new List<Light>();
        shipLightSources.AddRange(descendantLights);
        for (int i = 0; i < shipLightSources.Count; i++)
        {
            shipLightSources[i].enabled = true;
        }
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnBreakerActive.RemoveListener(TurnOnLights);
    }
}
