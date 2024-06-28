using UnityEngine;
using System.Collections;

public class BlinkButton : MonoBehaviour
{
    public Material blinkingMaterial; // Assign the Material with Emission in the Inspector
    public AudioSource audioSource;   // Assign the AudioSource in the Inspector
    public AudioClip blinkSound;      // Assign the sound clip in the Inspector

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

    public void StopBlinking()
    {
        // Stop the coroutine (if it's currently running)
        StopCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Toggle the Emission of the material
            if (blinkingMaterial != null)
            {
                blinkingMaterial.EnableKeyword("_EMISSION");
                blinkingMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

                // Play the blink sound
                if (audioSource != null && blinkSound != null)
                {
                    audioSource.PlayOneShot(blinkSound);
                }

                // Wait for a short duration before toggling again
                yield return new WaitForSeconds(0.5f);

                blinkingMaterial.DisableKeyword("_EMISSION");
                blinkingMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;

                // Wait for a short duration before toggling again
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
