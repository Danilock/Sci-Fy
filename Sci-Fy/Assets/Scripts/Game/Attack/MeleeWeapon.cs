﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Melee Size")]
    [SerializeField, Range(0, 10)] private float _radius;
    [SerializeField] private Vector3 _offset;

    [Header("Damage")]
    [SerializeField, Range(0, 1)] private float _damage = .5f;
    [SerializeField] private LayerMask _damageableLayers;
    [SerializeField] private DamageSourceTeam _sourceTeam;
    [SerializeField] private AttackForce _force;

    [SerializeField] private GameObject _weaponOwner;

    [SerializeField] UnityEvent OnStartAttack;

    public bool UseMeleeAttack { get; private set; }

    private void FixedUpdate()
    {
        if (UseMeleeAttack)
        {
            DoAttack();
        }
    }
    void DoAttack()
    {
        Collider2D[] damageablesHitted = Physics2D.OverlapCircleAll(
            CalculateAttackAreaPosition(transform.position),
            _radius,
            _damageableLayers
            );

        foreach(Collider2D colHitted in damageablesHitted)
        {
            Damageable currentDamageable = colHitted.GetComponent<Damageable>();

            if (currentDamageable == null)
                return;

            currentDamageable.TakeDamage(_damage, _sourceTeam);
        }
    }

    public void SetOwner(GameObject newOwner)
    {
        _weaponOwner = newOwner;
    }

    public void StartAttack()
    {
        UseMeleeAttack = true;
        OnStartAttack.Invoke();
    }

    public void EndAttack() => UseMeleeAttack = false;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float weaponOwnerDirection = Mathf.Sign(_weaponOwner.transform.localScale.x);
        Gizmos.DrawWireSphere(CalculateAttackAreaPosition(transform.position), _radius);
        #if UNITY_EDITOR
        
        #endif  
    }

    Vector3 CalculateAttackAreaPosition(Vector3 reference)
    {
        Vector3 offsetCalculation = new Vector3(_offset.x * Mathf.Sign(_weaponOwner.transform.localScale.x),
                                                _offset.y * Mathf.Sign(_weaponOwner.transform.localScale.y),
                                                _offset.z * Mathf.Sign(_weaponOwner.transform.localScale.z));
        return reference + offsetCalculation;
    }
}
