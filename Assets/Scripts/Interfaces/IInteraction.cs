using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    public void Interact(Player player);
    public bool CanInteract();

    public void SetInteractable(bool interactable);
    
}
