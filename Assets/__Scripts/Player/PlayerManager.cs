using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private bool exit = false;
    //Camera To UI
    private Camera playerCam;
    private Coroutine camMoveCoroutine;
    private FirstPersonLook camScript;
    private FirstPersonMovement playerMoveScript;
    [SerializeField] float camMoveSpeed = 5f;

    private void Start()
    {
        camScript = GetComponentInChildren<FirstPersonLook>();
        playerMoveScript = GetComponent<FirstPersonMovement>();
        playerCam = GetComponentInChildren<Camera>();
        PlayerData.instance.SetPlayer(transform);
        PlayerData.instance.OnEnterUI.AddListener(MoveCamToUI);
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        exit = context.action.triggered;
    }
    void Update()
    {

        if (exit && PlayerData.instance.inUI)
        {
            MoveCamBackToPlayer();
        }
        if (exit && !PlayerData.instance.inUI)
        {
            //Application.Quit();
        }
    }

    void MoveCamToUI(Transform targetPos)
    {
        if (camMoveCoroutine != null) { StopCoroutine(camMoveCoroutine); }
        camMoveCoroutine = StartCoroutine(MoveCamToTargetPos(targetPos, false));
    }
    void MoveCamBackToPlayer()
    {
        if (camMoveCoroutine != null) { StopCoroutine(camMoveCoroutine); }
        camMoveCoroutine = StartCoroutine(MoveCamToTargetPos(camScript.baseCamPos, true));
    }
    private IEnumerator MoveCamToTargetPos(Transform targetPos, bool backToPlayer)
    {
        // Determine speed and margin based on the direction of movement
        float speed = backToPlayer ? camMoveSpeed * 2.5f : camMoveSpeed;
        float margin = 0.05f;

        // Set UI or Player mode based on the direction of movement
        Cursor.lockState = backToPlayer ? CursorLockMode.Locked : CursorLockMode.None;
        if (!backToPlayer)
        {
            Vector3 rotation = camScript.baseCamPos.rotation.eulerAngles;
            rotation.x = playerCam.transform.rotation.eulerAngles.x;
            camScript.baseCamPos.rotation = Quaternion.Euler(rotation);

            camScript.enabled = false;
            playerMoveScript.enabled = false;
            PlayerData.instance.inUI = true;
        }
        while (Vector3.Distance(playerCam.transform.position, targetPos.position) > margin || Quaternion.Angle(playerCam.transform.rotation, targetPos.rotation) > margin)
        {
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, targetPos.position, speed * Time.deltaTime);
            playerCam.transform.rotation = Quaternion.Slerp(playerCam.transform.rotation, targetPos.rotation, speed * Time.deltaTime);
            yield return null;
        }
        if (backToPlayer)
        {
            camScript.enabled = true;
            playerMoveScript.enabled = true;
            PlayerData.instance.inUI = false;
        }

    }

    private void OnDestroy()
    {
        PlayerData.instance.OnEnterUI.RemoveListener(MoveCamToUI);
    }
}
