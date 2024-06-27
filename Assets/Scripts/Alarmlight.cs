using UnityEngine;
using System.Collections;

public class Alarmlight : MonoBehaviour
{
    public Material alarmMaterial; // Assign your material with emission property
    public Light alarmLight; // Assign your light GameObject

    public float blinkInterval = 1f; // Time between blinks in seconds

    void Start()
    {
        // Start the coroutine to blink the alarm
        StartCoroutine(BlinkAlarm());
    }

    void Update()
    {
        // No need for input check if the alarm blinks indefinitely
    }

    IEnumerator BlinkAlarm()
    {
        while (true)
        {
            // Enable the light and emission
            EnableAlarm();

            // Wait for the specified blink interval
            yield return new WaitForSeconds(blinkInterval);

            // Disable the light and emission
            DisableAlarm();

            // Wait for the specified blink interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void EnableAlarm()
    {
        alarmMaterial.EnableKeyword("_EMISSION"); // Enable emission in the material
        alarmLight.enabled = true; // Enable the light
    }

    void DisableAlarm()
    {
        alarmMaterial.DisableKeyword("_EMISSION"); // Disable emission in the material
        alarmLight.enabled = false; // Disable the light
    }
}
