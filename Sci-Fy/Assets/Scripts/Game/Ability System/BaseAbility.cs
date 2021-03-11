using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Ability
{
    [RequireComponent(typeof(Mana))]
    public abstract class BaseAbility : MonoBehaviour
    {
        [Header("Ability Info")]
        public string Name;
        public Texture2D Icon;

        [SerializeField] float _cooldown;

        [SerializeField] float _manaRequired;
        private Mana _manaComponent;
        public float Cooldown { 
            get 
            { 
                return _cooldown; 
            } 
            private set 
            { 
                _cooldown = value; 
            } 
        }

        [SerializeField] bool _canUse = true;
        public bool CanUse {
            get => _canUse;
            set => _canUse = value;
        }

        public UnityEvent OnUseAbility;

        public virtual void Start()
        {
            _manaComponent = GetComponent<Mana>();
        }

        public void TriggerAbility()
        {
            if (!CanUse || _manaComponent.CurrentMana < _manaRequired)
                return;
            Ability();
            OnUseAbility.Invoke();
            StartCoroutine(StartCooldown());
            _manaComponent.RestMana(_manaRequired);
        }

        public abstract void Ability();

        IEnumerator StartCooldown()
        {
            CanUse = false;
            yield return new WaitForSeconds(Cooldown);
            CanUse = true;
        }

        public void SetCooldown(float newCooldown) => Cooldown = newCooldown;

        public void SetAbilityState(bool state) => CanUse = state;
    }
}