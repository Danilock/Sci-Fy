using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events{
    public class Event_OnTriggerEnter2D : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _onTriggerEnter2D;
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag(_tag)){
                _onTriggerEnter2D.Invoke();
            }
        }

        private void OnDisable() {
            _onTriggerEnter2D.RemoveAllListeners();
        }
    }
}