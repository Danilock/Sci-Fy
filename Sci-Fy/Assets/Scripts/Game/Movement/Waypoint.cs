using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    [System.Serializable]
    public class Waypoint
    {
        public Transform WayPoint;
        public Facing FaceDirection;
    }
}

public enum Facing
{
    Left, Right
}