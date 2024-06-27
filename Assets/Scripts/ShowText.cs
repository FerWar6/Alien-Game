using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public Text displayText;
    public float maxDistance = 5f;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Player is looking at the collider of this object
                ShowUIText();
            }
            else
            {
                // Player is looking away
                HideUIText();
            }
        }
        else
        {
            // Player is looking away
            HideUIText();
        }
    }

    void ShowUIText()
    {
        if (displayText != null)
        {
            displayText.enabled = true;
        }
    }

    void HideUIText()
    {
        if (displayText != null)
        {
            displayText.enabled = false;
        }
    }
}
