using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable_Terminal : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "E to start puzzle";

    private Transform UICamPos;
    [SerializeField] private Puzzle_RowManager rowMan;
    [HideInInspector] public UnityEvent<bool> OnTerminalStatusChange = new UnityEvent<bool>();
    public bool inThisTerminal = false;

    private void Start()
    {
        PlayerData.instance.OnExitUI.AddListener(ExitTerminal);
        if (rowMan != null) rowMan.OnPuzzleCompleted.AddListener(DestroyThis);
        else
        {
            Debug.Log("interactive terminal failed to link to row manager");
        }
    }
    private void Update()
    {
        UICamPos = GetComponentInChildren<UICamPos>().transform;
    }
    public void Interact()
    {
        if (!PlayerData.instance.inUI)
        {
            ChangeTerminalStatus(true);
            PlayerData.instance.OnEnterUI.Invoke(UICamPos);
        }
    }
    private void ExitTerminal()
    {
        if (inThisTerminal)
        {
            ChangeTerminalStatus(false);
        }
    }
    private void ChangeTerminalStatus(bool inTerminal)
    {
        inThisTerminal = inTerminal;
        OnTerminalStatusChange.Invoke(false);
    }
    void DestroyThis()
    {
        StartCoroutine(DestroyThisWithDelay(0.5f));
    }
    private IEnumerator DestroyThisWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this);
    }
    private void OnDestroy()
    {
        OnTerminalStatusChange.Invoke(true);
        PlayerData.instance.OnExitUI.RemoveListener(ExitTerminal);
    }
}
