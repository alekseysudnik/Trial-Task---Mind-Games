using System;
using UnityEngine;

public class ObjectInteractions
{
    private int pickableLayerMask = 1 << 6;
    private float raycastDistance;
    private IInteraction interactionObject;

    public event Action<IInteraction> OnInteractionDetected;

    public ObjectInteractions(float raycastDistance)
    {
        this.raycastDistance = raycastDistance;
    }

    private IInteraction DetectInteraction(Transform raycastSource)
    {
        if (Physics.Raycast(raycastSource.position, -raycastSource.up, out RaycastHit hit, raycastDistance, pickableLayerMask))
        {
            if (hit.transform.TryGetComponent(out IInteraction newInteraction))
            {
                if (newInteraction.CanInteract())
                {
                    return newInteraction;
                } 
            }
        }
        return null;
    }
    public void HandleInteractions(Transform raycastSource)
    {
        IInteraction interaction = DetectInteraction(raycastSource);

        if (interactionObject != interaction)
        {
            interactionObject = interaction;
            OnInteractionDetected?.Invoke(interaction);
        }
    }

    public void TriggerInteraction(Player player)
    {
        if (interactionObject.CanInteract())
        {
            interactionObject.Interact(player);
        }
    }

}
