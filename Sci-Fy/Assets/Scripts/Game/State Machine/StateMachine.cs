using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachine
{
    public class StateMachine<T>
    {
        T StateMachineOwner;
        public StateMachine(T stateMachineOwner)
        {
            StateMachineOwner = stateMachineOwner;
        }
        public IState<T> CurrentState
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Specify who is calling the method</param>
        /// <param name="newState"></param>
        public void SetState(IState<T> newState)
        {
            CurrentState?.ExitState(StateMachineOwner);
            CurrentState = newState;
            CurrentState.EnterState(StateMachineOwner);
        }
    }
}