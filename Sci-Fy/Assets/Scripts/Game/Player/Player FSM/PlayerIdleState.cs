﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerIdleState : IState<PlayerController> 
{ 

    public void EnterState(PlayerController player)
    {

    }

    public void ExitState(PlayerController player)
    {
        
    }

    public void TickState(PlayerController player)
    {
        player.CharacterController.Move(Input.GetAxisRaw("Horizontal"), false, false);

        if (player.Rigidbody.velocity.magnitude > 0.1f)
        {
            player.StateMachine.SetState(player.MovingState);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            //Making jump 
            player.CharacterController.Move(
                Input.GetAxisRaw("Horizontal"),
                false,
                true
                );
        }
    }
}
