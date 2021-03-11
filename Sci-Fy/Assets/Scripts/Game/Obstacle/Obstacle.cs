using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private string _targetTag;
    [SerializeField] private float _damageAmount;
    [SerializeField] private DamageSourceTeam _sourceTeam;
    private Collider2D _objectCollider;

    private void Start()
    {
        _objectCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_objectCollider.isTrigger)
            return;

        if (collision.gameObject.CompareTag(_targetTag))
        {
            DoDamage(collision.gameObject.GetComponent<Damageable>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_objectCollider.isTrigger)
            return;

        if (other.gameObject.CompareTag(_targetTag))
        {
            DoDamage(other.GetComponent<Damageable>());
        }
    }

    private void DoDamage(Damageable damageable)
    {
        if (damageable == null)
            return;

        damageable.TakeDamage(_damageAmount, _sourceTeam);
    }
}
