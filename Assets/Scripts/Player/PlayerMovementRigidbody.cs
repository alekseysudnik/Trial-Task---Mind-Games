using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRigidbody : IPlayerMovement
{
    private Player player;
    private Rigidbody playerRB;
    private float velocity;
    public PlayerMovementRigidbody(Player player, float velocity)
    { 
        this.player = player;
        this.velocity = velocity;      
    }

    public void Move(Vector3 direction)
    {
        if (playerRB == null)
        {
            playerRB = player.GetComponentInChildren<Rigidbody>();
        }
        try
        {
            playerRB.velocity = direction * velocity;
        }
        catch
        {
            Debug.LogError("No Player rigidbody was found");
        }

    }
}
