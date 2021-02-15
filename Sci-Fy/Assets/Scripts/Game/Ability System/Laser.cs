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

        [Header("Projectile")]
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _instantiatePoint;

        #region Laser Object Pooling
        const int LASER_POOL_SIZE = 5;
        private GameObject[] _laserPool = new GameObject[LASER_POOL_SIZE];
        private int _actualLaserIndexInPool;

        private static GameObject _poolParent;
        private static GameObject PoolParent 
        {
            get
            {
                if (_poolParent == null)
                    _poolParent = new GameObject("Laser Pool");
                
                return _poolParent;
            }
        }
        #endregion

        private void Start()
        {
            InitiateLaserPool();
        }

        private void InitiateLaserPool()
        {
            for(int i = 0; i < _laserPool.Length; i++)
            {
                _laserPool[i] = Instantiate(_projectile.gameObject, transform.position, Quaternion.identity);
                _laserPool[i].SetActive(false);
                _laserPool[i].transform.SetParent(PoolParent.transform);
            }
        }

        public override void Ability()
        {
            UseLaserPool();//Activating the laser effect

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

        private void DoLaserDamage(RaycastHit2D[] objectsHitted)
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

        public void HandleLaserInAnimation() => TriggerAbility();

        private void UseLaserPool()
        {
            _laserPool[_actualLaserIndexInPool].transform.position = _instantiatePoint.position;
            _laserPool[_actualLaserIndexInPool].SetActive(true);

            _actualLaserIndexInPool = (_actualLaserIndexInPool + 1) & _laserPool.Length;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green * .5f;

            Gizmos.DrawLine(transform.position, GetLaserEndPosition());
        }
    }
}