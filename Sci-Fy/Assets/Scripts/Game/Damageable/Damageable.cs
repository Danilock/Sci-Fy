using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: Need to use scriptable objects
public class Damageable : MonoBehaviour
{
    [Header("Team")]
    [SerializeField] DamageSourceTeam _team;
    [Space]

    #region Stats Info
    [Header("Stats")]
    [SerializeField, Range(0, 100)] private float _startHealth = 100f;
    [SerializeField, Range(0, 100)] private float _armor = 30f;
    public float CurrentHealth { get; private set; }
    public float CurrentArmor { get; private set; }
    public bool Invulnerable { get; private set; }
    #endregion

    [Header("On Dead Event")]
    [SerializeField] UnityEvent OnDead;

    private void Start()
    {
        CurrentHealth = _startHealth;
    }

    public void SetArmor(float armor)
    {
        CurrentArmor = armor;
    }

    public void SetLife(float life)
    {
        CurrentHealth = life;
    }

    public void TakeDamage(float damageAmount, DamageSourceTeam SourceTeam)
    {
        if (Invulnerable || SourceTeam == _team)
            return;

        CurrentHealth -= (damageAmount - _armor);

        if(CurrentHealth <= 0f)
        {
            OnDead.Invoke();
        }
    }
}

public enum DamageSourceTeam
{
    Friendly,
    Enemy,
    IgnoreTeam
}
