using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Movement
{
    public abstract class LerpMovement : MonoBehaviour
    {
        [Header("Lerp Movement Attributes")]
        public Transform StartPosition;
        public Transform EndPosition;
        [Range(0, 1)] public float Speed = .5f;
        [SerializeField, Range(0, 1)] private float LerpPct;

        public UnityEvent OnReachDistance;
        public UnityEvent OnStartMovement;

        public virtual void Move(Transform endPosition)
        {
            StartPosition = transform;
            EndPosition = endPosition;
            LerpPct = 0f;
            OnStartMovement.Invoke();
        }

        private void Update()
        {
            DoLerping();
            transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position, LerpPct);
        }

        private void DoLerping()
        {
            if (Vector2.Distance(EndPosition.position, transform.position) > 0.01f)
                LerpPct += 0.01f * Speed * Time.deltaTime;
            else
                OnReachDistance.Invoke();
        }
    }
}