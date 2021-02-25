using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(DamageableUI))]
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

    [SerializeField] private bool _invulnerable = false;
    public bool Invulnerable
    {
        get => _invulnerable;
        private set => _invulnerable = value;
    }
    #endregion

    [Header("Damageable Events")]
    public OnDeadEvent OnDead;
    public OnHitEvent OnTakeDamage;
    public UnityEvent OnTakeDamageWhenItsInvulnerable;

    private void Awake()
    {
        CurrentHealth = _startHealth;
        CurrentArmor = _startArmor;

        if (OnTakeDamage == null)
            OnTakeDamage = new OnHitEvent();

        if (OnDead == null)
            OnDead = new OnDeadEvent();
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
        if (SourceTeam == Team)
            return;

        if (Invulnerable)
        {
            OnTakeDamageWhenItsInvulnerable.Invoke();
            return;
        }

        CurrentHealth -= CalculateDamageByArmor(damageAmount, CurrentArmor);

        if (CurrentHealth <= 0f)
            OnDead.Invoke(damageAmount);
        else
            OnTakeDamage.Invoke(damageAmount);
    }

    /// <summary>
    /// Does damage ignoring the team
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(float damageAmount)
    {
        if (Invulnerable)
            return;

        CurrentHealth -= CalculateDamageByArmor(damageAmount, CurrentArmor);

        if (CurrentHealth <= 0f)
        {
            OnDead.Invoke(damageAmount);
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

    public void InstantiateOnDead(GameObject objToInstantiate) => Instantiate(objToInstantiate, transform.position, Quaternion.identity);
    [System.Serializable] public class OnHitEvent : UnityEvent<float> { }
    [System.Serializable] public class OnDeadEvent : UnityEvent<float> { }
}
public enum DamageSourceTeam
{
    Friendly,
    Enemy,
    IgnoreTeam
}