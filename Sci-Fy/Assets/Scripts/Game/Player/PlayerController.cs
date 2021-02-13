﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerController : Character<PlayerController>
{
    #region Player State Machine
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerMovingState MovingState = new PlayerMovingState();
    public PlayerJumpState JumpState = new PlayerJumpState();
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

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        CharacterController = GetComponent<CharacterController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        
        StateMachine.SetState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.TickState(this);
        Debug.Log(StateMachine.CurrentState);
    }
}
