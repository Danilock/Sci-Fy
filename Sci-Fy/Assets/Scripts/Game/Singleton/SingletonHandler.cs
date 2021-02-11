using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonHandler<T> : MonoBehaviour
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No instance");
            }

            return _instance;
        }
        protected set
        {
            _instance = value;
        }
    }

    private void Start()
    {
        InitializeSingleton();
    }

    public abstract void InitializeSingleton();
}
