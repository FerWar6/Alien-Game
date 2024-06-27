using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipLightsManager : MonoBehaviour
{
    // Declare a public UnityEvent field
    public UnityEvent onTurnOnLights;

    void Start()
    {
        // Optionally, initialize the event
        if (onTurnOnLights == null)
        {
            onTurnOnLights = new UnityEvent();
        }
    }

    void Update()
    {
        // For testing, you could trigger the event with a key press
        if (Input.GetKeyDown(KeyCode.T))
        {
            onTurnOnLights.Invoke();
        }
    }

    // The function to be called by the UnityEvent
    public void TurnOnLights()
    {
        // Add all found Light components to the alarmLightSources list
        Light[] descendantLights = GetComponentsInChildren<Light>();
        List<Light> shipLightSources = new List<Light>();
        shipLightSources.AddRange(descendantLights);
        for (int i = 0; i < shipLightSources.Count; i++)
        {
            shipLightSources[i].enabled = true;
        }
    }
}
