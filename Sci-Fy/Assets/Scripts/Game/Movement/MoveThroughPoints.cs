using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class MoveThroughPoints : LerpMovement
    {
        [Header("Waypoints")]
        [SerializeField] private Transform[] _wayPoints;
        private int _currentWaypointIndex = 0;

        private void Start()
        {
            Move(_wayPoints[0]);
            OnReachDistance.AddListener(SelectNextWaypoint);
        }

        public void SelectNextWaypoint()
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
            Move(_wayPoints[_currentWaypointIndex]);
        }

        //Drawing green lines to specify the end points of this object
        private void OnDrawGizmos()
        {
            if (_wayPoints.Length < 2)
                return;

            Gizmos.color = Color.green;
            
            for(int i = 0; i < _wayPoints.Length; i++)
            {
                Gizmos.DrawLine(_wayPoints[i].position, _wayPoints[(i + 1) % _wayPoints.Length].position);
                Gizmos.DrawSphere(_wayPoints[i].position, .2f);
            }
        }
    }
}