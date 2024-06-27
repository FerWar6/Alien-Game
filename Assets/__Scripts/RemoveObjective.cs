using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveObjective : MonoBehaviour
{
    public GameObject objectiveObject;
    public Text objectiveText;
    public GameObject breakerLights;
    

    private bool objectiveCompleted = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the objectiveObject is active
        if (objectiveObject != null && objectiveObject.activeSelf)
        {
            // Enable the objectiveText if it's disabled
            if (!objectiveText.gameObject.activeSelf)
            {
                objectiveText.gameObject.SetActive(true);
            }

            // Set the initial objective text
            if (!objectiveCompleted)
            {
                objectiveText.text = "Objective: Disable alarm (Cockpit)";
                objectiveCompleted = true;
            }
        }
        else
        {
           
            // Change the objective text when the alarm is disabled
            if (objectiveCompleted)
            {
                objectiveText.text = "Objective: Find a way to enable the ship's lights (Maintenance Room)";
                objectiveCompleted = false;
            }
        }
    }

    void enableLightsObjective()
    {
        if (breakerLights.gameObject.activeSelf)
        {
            objectiveCompleted = true;
        }
    }
}
