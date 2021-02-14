using System.Collections;
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


        if (Input.GetButtonDown("Jump") || !player.CharacterController.IsGrounded)
        {
            //Making jump 
            player.CharacterController.Move(
                Input.GetAxisRaw("Horizontal"),
                false,
                true
                );
            player.StateMachine.SetState(player.JumpState);
        }
        else if (player.Rigidbody.velocity.x != 0f && player.CharacterController.IsGrounded)
        {
            player.StateMachine.SetState(player.MovingState);
        }
    }
}
