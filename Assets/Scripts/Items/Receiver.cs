using System;
using UnityEngine;


public class Receiver : MonoBehaviour, IInteraction
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform receivePoint;
    [SerializeField] private ItemSO receiverSO;
    public Transform ReceivePoint { get => receivePoint; }
    private bool canInteract = true;

    public event Action<Receiver> OnReceiverInteract;
    public event Action<Receiver, IPickable> OnPickableUpdated;

    private IPickable pickableAtDisposal;
    public void SetPickableObject(IPickable pickableObject)
    {
        SetInteractable(false);
        pickableAtDisposal = pickableObject;
        pickableAtDisposal.OnItemPicked += PickableAtDisposal_OnItemPicked;
        OnPickableUpdated?.Invoke(this, pickableAtDisposal);
    }

    private void PickableAtDisposal_OnItemPicked()
    {
        SetInteractable(true);
        pickableAtDisposal.OnItemPicked-= PickableAtDisposal_OnItemPicked;
        OnPickableUpdated?.Invoke(this, null);
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public void Interact(Player player)
    {
        OnReceiverInteract?.Invoke(this);
    }

    public void SetInteractable(bool interactable)
    {
        canInteract = interactable;
    }

    private void OnDisable()
    {
        if (pickableAtDisposal != null)
        {
            pickableAtDisposal.OnItemPicked -= PickableAtDisposal_OnItemPicked;
        }
    }
}
