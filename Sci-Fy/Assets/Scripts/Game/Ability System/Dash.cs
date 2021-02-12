using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ability
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Dash : BaseAbility
    {
        [Header("Dash")]
        [SerializeField, Range(0, 2500)] float _force;
        [SerializeField] Sprite _characterSprite;
        Rigidbody2D _characterRgb;
        CharacterController2D _characterController;

        private void Start()
        {
            _characterRgb = GetComponent<Rigidbody2D>();
            _characterController = GetComponent<CharacterController2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TriggerAbility();
            }
        }
        public override void Ability()
        {
            StartCoroutine(HandleCharacterController());
            _characterRgb.AddForce(Vector2.right * CalculateCharacterDirection() * _force, ForceMode2D.Impulse);
            StartCoroutine(FadeEffect());
        }

        private float CalculateCharacterDirection() => transform.localScale.normalized.x;

        IEnumerator HandleCharacterController()
        {
            _characterController.CanMove = false;
            yield return new WaitForSeconds(.5f);
            _characterController.CanMove = true;
        }

        IEnumerator FadeEffect()
        {
            int i = 0;

            while(i < 10)
            {
                GameObject fadeObj = new GameObject("Fade");

                fadeObj.transform.position = transform.position;
                fadeObj.transform.localScale = new Vector3(2f, 2f, 1f);

                SpriteRenderer fadeSprite = fadeObj.AddComponent<SpriteRenderer>();

                fadeSprite.sprite = _characterSprite;
                fadeSprite.color = Color.white * .5f;

                i++;

                Destroy(fadeObj, 1f);
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}