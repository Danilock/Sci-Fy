using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public abstract class Character<T> : MonoBehaviour
{
    T character;
    public StateMachine<T> StateMachine { get; protected set; }

    public virtual void Start()
    {
        StateMachine = new StateMachine<T>(character);
    }
}
