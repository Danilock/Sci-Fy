using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerController : MonoBehaviour, ISetState<PlayerController>
{
    #region PlayerStates
    private IState<PlayerController> _currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerMovingState MovingState = new PlayerMovingState();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.TickState(this);
    }

    public void SetState(IState<PlayerController> newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }
}
