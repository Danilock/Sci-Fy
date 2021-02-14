using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerWalkState : IState<PlayerController>
{
    public void EnterState(PlayerController player)
    {
        player.AnimationHandler.TriggerWalkAnimation();
    }

    public void ExitState(PlayerController player)
    {
        
    }

    public void TickState(PlayerController player)
    {
        player.CharacterController.Move(Input.GetAxisRaw("Horizontal"), false, false);

        if (player.Rigidbody.velocity.magnitude < 0.01f)
        {
            player.StateMachine.SetState(player.IdleState);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            player.CharacterController.Move(
                Input.GetAxisRaw("Horizontal"),
                false,
                true
                );
            player.StateMachine.SetState(player.JumpState);
        }
    }
}
