using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;
public class PlayerNoPlayableState : IState<PlayerController>
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
        
    }
}
