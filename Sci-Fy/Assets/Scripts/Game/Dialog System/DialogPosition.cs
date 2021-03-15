using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DialogSystem
{
    [ExecuteInEditMode]
    public class DialogPosition : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmos;

        private void OnDrawGizmosSelected()
        {
            if (!_drawGizmos)
                return;

            Gizmos.color = Color.green;

            Gizmos.DrawCube(transform.position, new Vector3(2f, 2f));
        }
    }
}