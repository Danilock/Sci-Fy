using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

public abstract class Character<T> : MonoBehaviour
{
    T character;
    public StateMachine<T> StateMachine = new StateMachine<T>();
}
