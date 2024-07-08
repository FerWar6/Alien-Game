using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TerminalButtonHighlighter : MonoBehaviour
{
    private Color baseColor;
    private TextMeshProUGUI buttonText;
    private void Start()
    {
        SetAlpha(0);
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        baseColor = buttonText.color;
    }
    public void OnEnterButton()
    {
        SetAlpha(1);
        buttonText.color = Color.black;
    }
    public void OnExitButton()
    {
        SetAlpha(0);
        buttonText.color = baseColor;
    }
    private void SetAlpha(int alpha)
    {
        Image buttonSprite = GetComponent<Image>();
        Color newColor = baseColor;
        newColor.a = alpha;
        buttonSprite.color = newColor;
    }
}
