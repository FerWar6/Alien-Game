using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;

    private PlayerUI playerUI;
    private PlayerControls inputManager;

    private bool interacted = false;
    private bool canInteract = true;

    private Color testColor = Color.red;
    private void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
        inputManager = new PlayerControls();
    }
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, testColor);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            if ((mask & (1 << hitInfo.collider.gameObject.layer)) != 0)
            {

                if (hitInfo.collider.GetComponent<IInteractable>() != null)
                {

                    IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                    playerUI.UpdateText(hitInfo.collider.GetComponent<IInteractable>().promptMessage);
                    if (interacted && canInteract)
                    {
                        interactable.Interact();

                        canInteract = false;
                        StartCoroutine(InteractCooldown());
                    }
                }
                testColor = Color.green;
            }
            else
            {
                testColor = Color.red;
            }
        }
    }
    private IEnumerator InteractCooldown()
    {

        yield return new WaitForSeconds(0.25f);
        canInteract = true;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interacted = context.action.triggered;
    }
}