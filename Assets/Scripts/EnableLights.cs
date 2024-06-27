using UnityEngine;

public class EnableLights : MonoBehaviour
{
    public GameObject objectToActivate;
    public AudioSource activationAudioSource;
    public float maxDistance = 5f;
    public Material emissiveMat;
    private bool hasBeenActivated = false;

    private void Start()
    {
        emissiveMat.DisableKeyword("_EMISSION");
    }
    void Update()
    {
        if (!hasBeenActivated && Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Player is looking at the collider of this object and pressed E
                    ActivateTargetObject();
                }
            }
        }
    }

    void ActivateTargetObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            emissiveMat.EnableKeyword("_EMISSION");
        }

        if (activationAudioSource != null)
        {
            activationAudioSource.Play();
        }

        hasBeenActivated = true;
    }
}
