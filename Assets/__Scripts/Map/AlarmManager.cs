using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    private List<Light> alarmLightSources = new List<Light>(); //List of all the alarm Lights
    private AudioSource alarmAudioSource; // Assign the AudioSource in the Inspector

    [SerializeField] float cooldownTime; //time between alarm 
    private bool alarmActive = true;

    [SerializeField] Material alarmLightMaterial; //time between alarm 

    void Start()
    {
        PlayerData.instance.OnAlarmDeactivate.AddListener(TurnOffAlarm);
        alarmAudioSource = GetComponent<AudioSource>();
        //find the alarm Lights
        FindLights();
        // Start blinking when the script starts
        StartCoroutine(Blink());
    }
    void FindLights()
    {
        Light[] descendantLights = GetComponentsInChildren<Light>();

        // Add all found Light components to the alarmLightSources list
        alarmLightSources.AddRange(descendantLights);
    }
    IEnumerator Blink()
    {
        bool lightsOn = true;
        while (true)
        {
            // turn lights on and off
            if (alarmLightSources != null)
            {
                for (int i = 0; i < alarmLightSources.Count; i++)
                {
                    alarmLightSources[i].enabled = lightsOn;
                }
                lightsOn = !lightsOn;
            }

            // Play the alarm sound when the GameObject is enabled
            if (lightsOn)
            {
                if (alarmAudioSource != null)
                {
                    alarmAudioSource.Play();
                }
            }
            if (!lightsOn) alarmAudioSource.Stop();

            // Wait for a short duration before toggling again
            yield return new WaitForSeconds(cooldownTime);
        }
    }
    private void TurnOffAlarm()
    {
        for (int i = 0; i < alarmLightSources.Count; i++)
        {
            alarmLightSources[i].enabled = false;
        }
        alarmLightMaterial.DisableKeyword("_EMISSION");
        StopAllCoroutines();
        alarmAudioSource.Stop();
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnAlarmDeactivate.RemoveListener(TurnOffAlarm);
    }
}
