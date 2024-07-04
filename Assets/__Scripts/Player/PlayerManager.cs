using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform menuPos;

    private bool exit = false;
    //Camera To UI
    private Camera playerCam;
    private Coroutine camMoveCoroutine;
    private FirstPersonLook camScript;
    private FirstPersonMovement playerMoveScript;
    private float camMoveSpeed = 5f;
    private bool canExitUI = true;
    private void Start()
    {
        camScript = GetComponentInChildren<FirstPersonLook>();
        playerMoveScript = GetComponent<FirstPersonMovement>();
        playerCam = GetComponentInChildren<Camera>();
        PlayerData.instance.SetPlayer(transform);
        PlayerData.instance.OnEnterUI.AddListener(MoveCamToUI);
        PlayerData.instance.OnExitUI.AddListener(MoveCamBackToPlayer);
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        exit = context.action.triggered;
    }
    void Update()
    {
        if (exit && PlayerData.instance.inUI && canExitUI)
        {
            PlayerData.instance.OnExitUI.Invoke();
        }
        if (exit && !PlayerData.instance.inUI)
        {
            GetComponent<PlayerUI>().OpenPlayerMenu();
        }
    }

    void MoveCamToUI(Transform targetPos)
    {
        canExitUI = false;
        StartCoroutine(DelayExit());

        SetBaseCamRotation();

        if (camMoveCoroutine != null) { StopCoroutine(camMoveCoroutine); }
        camMoveCoroutine = StartCoroutine(MoveCamToTargetPos(targetPos, 5, false));
    }
    void MoveCamBackToPlayer()
    {
        if (camMoveCoroutine != null) { StopCoroutine(camMoveCoroutine); }
        camMoveCoroutine = StartCoroutine(MoveCamToTargetPos(camScript.baseCamPos, 15, true));
    }
    private IEnumerator MoveCamToTargetPos(Transform targetPos, float speed, bool backToPlayer)
    {
        float margin = 0.05f;

        Cursor.lockState = backToPlayer ? CursorLockMode.Locked : CursorLockMode.None;

        if (!backToPlayer) { SetScripts(false); }
        while (Vector3.Distance(playerCam.transform.position, targetPos.position) > margin || Quaternion.Angle(playerCam.transform.rotation, targetPos.rotation) > margin)
        {
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, targetPos.position, speed * Time.deltaTime);
            playerCam.transform.rotation = Quaternion.Slerp(playerCam.transform.rotation, targetPos.rotation, speed * Time.deltaTime);
            yield return null;
        }
        if (backToPlayer) { SetScripts(true); }
    }
    private IEnumerator DelayExit()
    {
        yield return new WaitForSeconds(0.2f);
        canExitUI = true;
    }
    private void SetScripts(bool enableScripts)
    {
        camScript.enabled = enableScripts;
        playerMoveScript.enabled = enableScripts;
        PlayerData.instance.inUI = !enableScripts;
    }
    private void SetBaseCamRotation()
    {
        Vector3 rotation = camScript.baseCamPos.rotation.eulerAngles;
        rotation.x = playerCam.transform.rotation.eulerAngles.x;
        camScript.baseCamPos.rotation = Quaternion.Euler(rotation);
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnEnterUI.RemoveListener(MoveCamToUI);
        PlayerData.instance.OnExitUI.RemoveListener(MoveCamBackToPlayer);
    }
}
