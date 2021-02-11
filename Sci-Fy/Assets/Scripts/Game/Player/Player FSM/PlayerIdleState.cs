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
        if (Input.GetButtonDown("Horizontal"))
        {
            player.SetState(player.MovingState);
        }
    }
}
