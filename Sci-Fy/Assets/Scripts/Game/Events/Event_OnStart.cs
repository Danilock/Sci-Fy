using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class Event_OnStart : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStart;

        private void Start()
        {
            OnStart.Invoke();
        }
    }
}