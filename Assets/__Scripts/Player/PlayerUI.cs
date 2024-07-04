using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    private PlayerMenuManager playerMenu;
    private void Start()
    {
        PlayerData.instance.OnEnterUI.AddListener(OnEnterUI);
        PlayerData.instance.OnExitUI.AddListener(OnExitUI);
        playerMenu = GetComponentInChildren<PlayerMenuManager>();
        playerMenu.gameObject.SetActive(false);
    }
    public void UpdateText(string promptMessage)
    {
        if(promptText != null)
        {
            promptText.text = promptMessage;
        }
    }
    public void Update()
    {
        promptText.enabled = !PlayerData.instance.inUI;
    }
    public void OpenPlayerMenu()
    {
        playerMenu.gameObject.SetActive(true);
        PlayerData.instance.OnEnterUI.Invoke(playerMenu.UICamPos);
    }
    private void OnEnterUI(Transform useless)
    {
        promptText.enabled = false;
    }
    private void OnExitUI()
    {
        playerMenu.gameObject.SetActive(false);
        promptText.enabled = true;
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnEnterUI.RemoveListener(OnEnterUI);
        PlayerData.instance.OnExitUI.RemoveListener(OnExitUI);
    }
}
