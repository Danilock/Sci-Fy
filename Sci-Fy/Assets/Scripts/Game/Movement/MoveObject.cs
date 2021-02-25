using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement {
    public class MoveObject : LerpMovement
    {
        public override void Move(Transform endPosition)
        {
            base.Move(endPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(StartPosition.position, EndPosition.position);
            Gizmos.DrawSphere(StartPosition.position, .3f);
            Gizmos.DrawSphere(EndPosition.position, .3f);
        }
    }
}