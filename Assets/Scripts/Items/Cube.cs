using System;
using UnityEngine;


public class Cube : MonoBehaviour, IPickable, IInteraction
{
    [SerializeField] private Rigidbody cubeRigidbody;
    [SerializeField] private Collider cubeCollider;
    [SerializeField] private float pickTime;
    [SerializeField] private ItemSO cubeSO;

    private Player player;

    private bool canInteract = true;

    public event Action OnItemPicked;

    public void Interact(Player player)
    {
        this.player = player;
        PickUp(player.MovingObject);
        canInteract = false;
    }
    public async void PickUp(Transform picker)
    {
        EnablePhysics(false);
        await ObjectModifier.ShrinkAndLift(1f, 0f, transform, picker, pickTime);
        player.inventory.AddItem(cubeSO, 1);
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
    public async void PutDown(Receiver receiver)
    {
        EnablePhysics(false);
        await ObjectModifier.ShrinkAndLift(0f, 1f, transform, receiver.ReceivePoint, pickTime);
        EnablePhysics(true);
        receiver.SetPickableObject(this);
        transform.SetParent(receiver.ReceivePoint);
    }
    private void EnablePhysics(bool enable)
    {
        SetInteractable(enable);
        cubeCollider.enabled = enable;
        cubeRigidbody.useGravity = enable;
        cubeRigidbody.freezeRotation = enable;
    }
    public bool CanInteract()
    {
        return canInteract;
    }

    public ItemSO GetPickableSO()
    {
        return cubeSO;
    }
    public void SetInteractable(bool interactable)
    {
        canInteract = interactable;
    }

}
