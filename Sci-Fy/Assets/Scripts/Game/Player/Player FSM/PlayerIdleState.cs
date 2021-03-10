using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerIdleState : IState<PlayerController> 
{ 

    public void EnterState(PlayerController player)
    {
        player.AnimationHandler.TriggerIdleAnimation();
    }

    public void ExitState(PlayerController player)
    {
        
    }

    public void TickState(PlayerController player)
    {
        player.CharacterController.Move(player.Move.x, false, false);


        if (player.Input.PlayerControls.Jump.triggered || !player.CharacterController.IsGrounded)
        {
            //Making jump 
            player.CharacterController.Move(
                player.Move.x,
                false,
                true
                );
            player.StateMachine.SetState(player.JumpState);
        }
        else if (player.Rigidbody.velocity.x != 0f && player.CharacterController.IsGrounded)
        {
            player.StateMachine.SetState(player.WalkState);
        }
    }
}
