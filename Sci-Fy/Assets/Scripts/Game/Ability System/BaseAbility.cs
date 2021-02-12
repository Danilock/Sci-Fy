using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ability
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [Header("Ability Info")]
        public string Name;
        public Texture2D Icon;

        [SerializeField] float _cooldown;
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
            get 
            { 
                return _canUse; 
            } 
            private set 
            { 
                _canUse = value; 
            } 
        }

        public void TriggerAbility()
        {
            if (!CanUse)
                return;
            Ability();
            StartCoroutine(StartCooldown());
        }

        public abstract void Ability();

        IEnumerator StartCooldown()
        {
            CanUse = false;
            yield return new WaitForSeconds(Cooldown);
            CanUse = true;
        }

        public void SetCooldown(float newCooldown) => Cooldown = newCooldown;
    }
}