using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Game.Movement
{
    public class MoveThroughPoints : LerpMovement
    {
        [Header("Waypoints")]
        [SerializeField] private Transform[] _wayPoints;
        private int _currentWaypointIndex = 0;

        [Header("Object Facing")]
        [SerializeField] private bool _canFlip;
        private CharacterController2D _ch2D;

        private void Start()
        {
            _ch2D = GetComponent<CharacterController2D>();
            OnReachDistance.AddListener(SelectNextWaypoint);
            Move(_wayPoints[0]);
        }

        public void SelectNextWaypoint()
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
            Move(_wayPoints[_currentWaypointIndex]);

            if (_canFlip)
                Flip(_wayPoints[_currentWaypointIndex]) ;
        }

        private void Flip(Transform wayPoint)
        {
            if (wayPoint.transform.position.x < transform.position.x && !_ch2D.FacingRight)
                _ch2D.Flip();
            else if (wayPoint.transform.position.x > transform.position.x && _ch2D.FacingRight)
                _ch2D.Flip();
        }

        //Drawing green lines to specify the end points of this object
        private void OnDrawGizmos()
        {
            if (_wayPoints.Length < 2 || _wayPoints == null)
                return;

            Gizmos.color = Color.green;
            
            for(int i = 0; i < _wayPoints.Length; i++)
            {
                Gizmos.DrawLine(_wayPoints[i].position, _wayPoints[(i + 1) % _wayPoints.Length].position);
                Gizmos.DrawSphere(_wayPoints[i].position, .2f);

                #if UNITY_EDITOR
                Handles.Label(_wayPoints[i].position + (Vector3.up * .3f), (i + 1).ToString());
                #endif
            }
        }
    }
}