using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject flashlight;
    

    private void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the active state of the GameObject
            if (flashlight != null)
            {
                flashlight.SetActive(!flashlight.activeSelf);
            }
            else
            {
                Debug.LogWarning("No GameObject assigned to toggle.");
            }
        }
    }
}
