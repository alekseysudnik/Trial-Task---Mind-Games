using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
    private Player player;
    private bool canInteract;
    GameInput gameInputActions;
    public PlayerController(Player player)
    { 
        this.player = player;
        canInteract = false;       
    }

    public void Initialize()
    {
        player.objectInteractions.OnInteractionDetected += ObjectInteractions_OnInteractionDetected;
        gameInputActions = new GameInput();
        gameInputActions.Player.Enable();
    }

    private void ObjectInteractions_OnInteractionDetected(IInteraction interaction)
    {
        canInteract = interaction != null;
    }

    public void UpdateController()
    {
            player.playerMovement.Move(GetMotionVector());
            ListenToInteraction();
    }

    private Vector3 GetMotionVector()
    {
        Vector3 direction = Vector3.zero;
        Vector2 movementVector = gameInputActions.Player.Move.ReadValue<Vector2>();
        direction.x = movementVector.x;
        direction.z = movementVector.y;

        return direction.normalized;
    }

    private void ListenToInteraction()
    {
        if (canInteract)
        {
            if(gameInputActions.Player.Interact.triggered)
            { 
                player.objectInteractions.TriggerInteraction(player);
                canInteract = false;
            }
        }
    }



}
