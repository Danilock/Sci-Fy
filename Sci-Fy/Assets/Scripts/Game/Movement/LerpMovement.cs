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
        public bool MoveOnGameStart = false;
        [Range(0, 1)] public float Speed = .5f;
        private float LerpPct;

        public UnityEvent OnReachDistance;
        public UnityEvent OnStartMovement;

        private void Start()
        {
            if (MoveOnGameStart)
                StartCoroutine(Move());
        }

        public virtual void Move(Transform endPosition)
        {
            StartPosition = transform;
            EndPosition = endPosition;
            LerpPct = 0f;
            OnStartMovement.Invoke();

            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            while(Vector2.Distance(EndPosition.position, transform.position) > .2f)
            {
                LerpPct += (.001f * Speed) / 100;
                transform.position = Vector2.Lerp(StartPosition.position,
                                                  EndPosition.position, LerpPct);
                yield return null;
            }

            OnReachDistance.Invoke();
        }
    }
}