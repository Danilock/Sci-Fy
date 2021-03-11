using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Movement
{
    public abstract class LerpMovement : MonoBehaviour
    {
        [Header("Lerp Movement Attributes")]
        [HideInInspector] public Transform StartPosition;
        [HideInInspector] public Transform EndPosition;
        [Range(0, 100)] public float Speed = .5f;

        public UnityEvent OnReachDistance;
        public UnityEvent OnStartMovement;

        private float _startTime;

        private float _lerpPct;

        [SerializeField] private bool _isStopped;

        public bool IsStopped {
            get => _isStopped;
            set{
                _isStopped = value;

                if(_isStopped)
                    _startTime = Time.time;
            }
        }

        public virtual void Start() {
            
            _startTime = Time.time;
        }

        public virtual void Move(Transform endPosition)
        {
            _lerpPct = 0f;
            _startTime = Time.time;

            StartPosition = transform;
            EndPosition = endPosition;
            OnStartMovement.Invoke();
        }

        private void Update()
        {
            if(!_isStopped){
                DoLerping();
            }
        }

        private void DoLerping()
        {
           _lerpPct += (Speed / 1000) * Time.deltaTime;

            transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position, _lerpPct);

            if(Vector3.Distance(StartPosition.position, EndPosition.position) < .2f)
                OnReachDistance.Invoke();
        }
    }
}