using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] float _obstacleDamageWhenCollide = 10f;
    [SerializeField] DamageSourceTeam _obstacleTeam;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable colDamageable = collision.gameObject.GetComponent<Damageable>();

        if (colDamageable == null)
            return;

        colDamageable.TakeDamage(_obstacleDamageWhenCollide, _obstacleTeam);
    }
}
