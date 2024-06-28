using UnityEngine;

public interface IInteractable
{
    string promptMessage { get; }
    void Interact();
}