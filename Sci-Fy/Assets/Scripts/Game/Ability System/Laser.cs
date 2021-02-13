using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ability {
    public class Laser : BaseAbility, IAttack
    {
        [Space]
        [Header("Laser Attributes")]
        [SerializeField, Range(0, 20)] private float _laserSize;

        #region Laser Damage
        [Header("Laser Damage")]
        [SerializeField] float _laserDamage;
        [SerializeField] DamageSourceTeam _laserOwnerTeam;
        [SerializeField, Tooltip("Wich layer can be damaged by the laser")] LayerMask _damageLayers;
        public LayerMask DamageLayers { get { return _damageLayers; } set => _damageLayers = value; }

        [SerializeField] private AttackForce _attackForce;
        public AttackForce AttackForceSetter
        {
            get
            {
                return _attackForce;
            }
            set
            {
                _attackForce = value;
            }
        }
        #endregion

        public override void Ability()
        {
            RaycastHit2D[] enemiesHitted = Physics2D.LinecastAll(transform.position, GetLaserEndPosition(), DamageLayers);

            if (enemiesHitted.Length > 0)
                DoLaserDamage(enemiesHitted);
        }

        private Vector3 GetLaserEndPosition()
        {
            float facingDirection = Mathf.Sign(transform.localScale.x);
            return new Vector3(transform.position.x + (_laserSize * facingDirection),
                                                        transform.position.y, 
                                                        transform.position.z);
        }

        void DoLaserDamage(RaycastHit2D[] objectsHitted)
        {
            foreach(RaycastHit2D actualObjectHitted in objectsHitted)
            {
                Damageable damageable = actualObjectHitted.collider.GetComponent<Damageable>();

                if (damageable == null)
                    return;

                damageable.GetComponent<SpriteRenderer>().color = Color.black;
                damageable.TakeDamage(_laserDamage, _laserOwnerTeam);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green * .5f;

            Gizmos.DrawLine(transform.position, GetLaserEndPosition());
        }
    }
}