using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    public GameObject alarmedObject; // Assign the GameObject with MeshRenderer in the Inspector
    public AudioSource alarmAudioSource; // Assign the AudioSource in the Inspector

    private bool isBlinking = false;

    public float time = 2f;

    void Start()
    {
        // Start blinking when the script starts
        StartBlinking();
    }

    void StartBlinking()
    {
        // Start a coroutine to handle the blinking behavior
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Toggle the state of the MeshRenderer component on the GameObject
            if (alarmedObject != null)
            {
                alarmedObject.SetActive(!alarmedObject.activeSelf);
            }

            // Play the alarm sound when the GameObject is enabled
            if (alarmedObject != null && alarmedObject.activeSelf)
            {
                if (alarmAudioSource != null)
                {
                    alarmAudioSource.Play();
                }
            }

            // Wait for a short duration before toggling again
            yield return new WaitForSeconds(time);
        }
    }
}
