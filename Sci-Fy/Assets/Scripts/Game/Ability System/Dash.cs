using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ability
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
    public class Dash : BaseAbility
    {
        [Header("Dash Attributes")]
        [SerializeField, Range(0, 2500)] float _force;
        [SerializeField, Tooltip("Shadows that will appear when dashing")] Sprite[] _shadow;

        private Rigidbody2D _rgb;
        private CharacterController2D _characterController;
        private Damageable _damageable;

        private void Awake()
        {
            _rgb = GetComponent<Rigidbody2D>();
            _characterController = GetComponent<CharacterController2D>();
            _damageable = GetComponent<Damageable>();
        }

        public override void Ability()
        {
            _damageable.SetInvulnerableByXSeconds(2f);

            StartCoroutine(HandleCharacterController());
            _rgb.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * _force, Input.GetAxisRaw("Vertical") * _force);
            StartCoroutine(FadeEffect());
        }

        private float CalculateCharacterDirection() => transform.localScale.normalized.x;

        IEnumerator HandleCharacterController()
        {
            _characterController.CanMove = false;
            yield return new WaitForSeconds(.5f);
            _characterController.CanMove = true;
        }

        /// <summary>
        /// instantiate 10 shadows in the position of the character to produce a shadow effect
        /// while doing the dash.
        /// </summary>
        /// <returns></returns>
        IEnumerator FadeEffect()
        {
            int i = 0;

            while(i < _shadow.Length)
            {
                GameObject fadeObj = new GameObject("Fade");

                fadeObj.transform.position = transform.position;
                fadeObj.transform.localScale = transform.localScale;

                SpriteRenderer fadeSprite = fadeObj.AddComponent<SpriteRenderer>();

                fadeSprite.sprite = _shadow[i];
                fadeSprite.color = Color.white * .5f;

                i++;

                Destroy(fadeObj, .5f);
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}