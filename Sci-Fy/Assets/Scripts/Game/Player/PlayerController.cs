using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerController : Character<PlayerController>
{
    #region Player State Machine
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerWalkState WalkState = new PlayerWalkState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerAttackState AttackState = new PlayerAttackState();
    #endregion

    #region CharacterController
    private CharacterController2D _characterController;
    public CharacterController2D CharacterController
    {
        get
        {
            if (_characterController == null)
                return null;
            return _characterController;
        }
        private set 
        {
            _characterController = value;
        }
    }

    [HideInInspector] public Rigidbody2D Rigidbody { get; private set; }
    #endregion
    [HideInInspector] public PlayerAnimationHandler AnimationHandler;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        AnimationHandler = GetComponent<PlayerAnimationHandler>();

        StateMachine.StateMachineOwner = this;
        StateMachine.SetState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.TickState(this);
    }

    public void SetPlayerState(PlayerStates newPlayerState)
    {
        switch (newPlayerState)
        {
            case PlayerStates.Idle:
                StateMachine.SetState(IdleState);
                break;
            case PlayerStates.Walk:
                StateMachine.SetState(WalkState);
                break;
            case PlayerStates.Jump:
                StateMachine.SetState(JumpState);
                break;
            case PlayerStates.Attack:
                StateMachine.SetState(AttackState);
                break;
        }
    }

    public enum PlayerStates { Idle, Walk, Jump, Attack }
}
