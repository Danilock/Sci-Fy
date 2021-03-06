using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events{
    public class Event_OnTriggerEnter2D : MonoBehaviour
    {
        [SerializeField] private string otherTagTarget;
        [SerializeField] private UnityEvent OnTriggerEnter2DHandler;
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag(otherTagTarget)){
                OnTriggerEnter2DHandler.Invoke();
            }
        }

        private void OnDisable() {
            OnTriggerEnter2DHandler.RemoveAllListeners();
        }
    }
}