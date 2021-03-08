using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class Event_OnCollisionEnter2D : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _onCollisionEnter;

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.CompareTag(_tag)){
                _onCollisionEnter.Invoke();
            }
        }
    }
}