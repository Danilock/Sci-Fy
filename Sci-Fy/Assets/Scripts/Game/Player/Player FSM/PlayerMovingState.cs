using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerMovingState : IState<PlayerController>
{
    public void EnterState(PlayerController type)
    {
        Debug.Log("Enter moving state");
    }

    public void ExitState(PlayerController type)
    {
        
    }

    public void TickState(PlayerController type)
    {
        
    }
}
