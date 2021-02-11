using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : SingletonHandler<PlayerInputController>
{
    public override void InitializeSingleton()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
