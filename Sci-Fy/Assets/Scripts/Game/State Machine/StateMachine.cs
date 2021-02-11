using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachine
{
    public class StateMachine<T>
    {
        public IState<T> CurrentState
        {
            get;
            private set;
        }

        public void SetState(IState<T> newState)
        {
            CurrentState = newState;
        }
    }
}