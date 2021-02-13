using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [Header("Team")]
    public DamageSourceTeam Team;
    [Space]

    #region Stats Info
    [Header("Stats")]
    [SerializeField, Range(0, 100)] private float _startHealth = 100f;
    [SerializeField, Range(0, 100)] private float _startArmor = 30f;
    public float CurrentHealth { get; private set; }
    public float CurrentArmor { get; private set; }
    public bool Invulnerable { get; private set; }
    #endregion

    [Header("On Dead Event")]
    [SerializeField] UnityEvent OnDead;

    private void Start()
    {
        CurrentHealth = _startHealth;
        CurrentArmor = _startArmor;
    }

    public void SetArmor(float armor)
    {
        CurrentArmor = armor;
    }

    public void SetHealth(float life)
    {
        CurrentHealth = life;
    }

    public void TakeDamage(float damageAmount, DamageSourceTeam SourceTeam)
    {
        if (Invulnerable || SourceTeam == Team)
            return;

        CurrentHealth -= CalculateDamageByArmor(damageAmount, CurrentArmor);

        if(CurrentHealth <= 0f)
        {
            OnDead.Invoke();
        }
    }

    float CalculateDamageByArmor(float damage, float armor)
    {
        if (armor > damage)
            return 1f;
        else
            return damage - armor;
    }

    public void SetInvulnerableByXSeconds(float seconds) => StartCoroutine(Invulnerability(seconds));

    IEnumerator Invulnerability(float seconds)
    {
        Invulnerable = true;
        yield return new WaitForSeconds(seconds);
        Invulnerable = false;
    }
}

public enum DamageSourceTeam
{
    Friendly,
    Enemy,
    IgnoreTeam
}
