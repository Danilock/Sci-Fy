using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public class PlayerController : MonoBehaviour
{
    #region Player State Machine
    public StateMachine<PlayerController> StateMachine;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerMovingState MovingState = new PlayerMovingState();
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
    void Start()
    {
        StateMachine = new StateMachine<PlayerController>();
        CharacterController = GetComponent<CharacterController2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        
        StateMachine.SetState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.TickState(this);
    }
}
