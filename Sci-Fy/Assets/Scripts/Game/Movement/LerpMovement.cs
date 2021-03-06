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
        [Range(0, 1)] public float Speed = .5f;

        public UnityEvent OnReachDistance;
        public UnityEvent OnStartMovement;

        private float _startTime;
        private float _journeyLength;

        private void Start() {
            
            _startTime = Time.time;
        }

        public virtual void Move(Transform endPosition)
        {
            StartPosition = transform;
            EndPosition = endPosition;
            OnStartMovement.Invoke();

            _journeyLength = Vector2.Distance(StartPosition.position, endPosition.position);
        }

        private void Update()
        {
            DoLerping();
        }

        private void DoLerping()
        {
            float distCovered = (Time.time - _startTime) * Speed;

            float fractionOfJourney = distCovered / _journeyLength;

            transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position, fractionOfJourney);
        }
    }
}