using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events{
    public class Event_OnUpdate : MonoBehaviour
    {
        [SerializeField] private UnityEvent _OnUpdate;
        // Update is called once per frame
        void Update()
        {
            _OnUpdate.Invoke();
        }
    }
}