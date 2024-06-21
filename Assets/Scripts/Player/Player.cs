using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Inject] ItemsFactory itemsFactory;
    [SerializeField] private Transform movingObject;
    public Transform MovingObject { get => movingObject; }

    public readonly IPlayerMovement playerMovement;
    public readonly ObjectInteractions objectInteractions;
    public readonly Inventory inventory;
    public readonly PlayerController controller;

    public Player()
    {
        controller = new PlayerController(this);
        playerMovement = new PlayerMovementRigidbody(this, 6f);
        objectInteractions = new ObjectInteractions(20f);
        inventory = new Inventory();
    }

    private void Start()
    {
        controller.Initialize();

    }

    private void FixedUpdate()
    {
        objectInteractions.HandleInteractions(movingObject);
        controller.UpdateController();
    }

    public bool TryExtractObjectFromInventory(ItemSO itemSO, out IPickable extractedObject)
    {
        if (inventory.TryRemoveItem(itemSO, 1))
        {
            IPickable pickable = itemsFactory.CreateItem(itemSO.itemType, movingObject.position).GetComponent<IPickable>();
            extractedObject = pickable;
            return true;
        }
        extractedObject = null;
        return false;
    }





}
