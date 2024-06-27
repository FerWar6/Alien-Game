using UnityEngine;
using UnityEngine.UI;

public class DisableAlarm : MonoBehaviour
{
    public GameObject objectToDisable;
    // Assign the GameObject you want to disable
    public BlinkButton blinkButtonScript;
    public GameObject buttonBeep;
    public BlinkButton blinkButton;
    public Text pressText;
    public ShowText showTextScript;
    public Material blinkingMaterial;
    void Update()
    {
        // Check if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Perform raycast to check if the player is hovering over the collider
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider belongs to the GameObject you want to interact with
                if (hit.collider.gameObject == gameObject)
                {
                    
                    // Disable the specified GameObject
                    if (objectToDisable != null)
                    {
                        objectToDisable.SetActive(false);
                        blinkButton.StopAllCoroutines();                  
                        blinkButtonScript.enabled = false;
                        buttonBeep.SetActive(false);
                        pressText.enabled = false;
                        showTextScript.enabled = false;
                        blinkingMaterial.DisableKeyword("_EMISSION");

                    }
                }
            }
        }
    }
}
