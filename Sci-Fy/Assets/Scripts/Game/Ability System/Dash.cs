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
        [SerializeField] float _shadowTransparency = .5f;

        private Rigidbody2D _rgb;
        private CharacterController2D _characterController;
        private Damageable _damageable;
        private Vector2 _velocityBeforeDash;

        private void Awake()
        {
            _rgb = GetComponent<Rigidbody2D>();
            _characterController = GetComponent<CharacterController2D>();
            _damageable = GetComponent<Damageable>();
        }

        public override void Ability()
        {
            _velocityBeforeDash = _rgb.velocity;
            _damageable.SetInvulnerableByXSeconds(2f);


            StartCoroutine(HandleCharacterController());


            _rgb.AddForce(new Vector2(CalculateCharacterDirection().x * _force,
                                      CalculateCharacterDirection().y * (_force / 2)
                            ), ForceMode2D.Impulse);


            StartCoroutine(FadeEffect());
        }

        private Vector2 CalculateCharacterDirection()
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),
                                            Input.GetAxisRaw("Vertical"));

            return direction;
        }

        IEnumerator HandleCharacterController()
        {
            float lastGravityValue = _rgb.gravityScale;


            _characterController.CanMove = false;
            _rgb.velocity = Vector2.zero;
            _rgb.gravityScale = 0f;


            yield return new WaitForSeconds(.5f);


            _characterController.CanMove = true;
            _rgb.gravityScale = lastGravityValue;
            //_rgb.velocity = _velocityBeforeDash / 2;
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
                fadeSprite.color = Color.white * _shadowTransparency;
                fadeSprite.sortingLayerName = "Characters";

                i++;

                Destroy(fadeObj, .5f);
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}