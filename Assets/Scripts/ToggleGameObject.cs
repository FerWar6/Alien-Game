using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        // Check for input, e.g., button press
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the active state of the targetObject
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}
