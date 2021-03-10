using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerJumpState : IState<PlayerController>
{
    public void EnterState(PlayerController player)
    {
        player.AnimationHandler.TriggerJumpAnimation();
    }

    public void ExitState(PlayerController player)
    {
       
    }

    public void TickState(PlayerController player)
    {
        player.CharacterController.Move(player.Move.x, false, false);

        if (player.Rigidbody.velocity.y <= 1f && player.CharacterController.IsGrounded)
        {
            player.StateMachine.SetState(player.IdleState);
        }
    }
}
