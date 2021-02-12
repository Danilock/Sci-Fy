using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ability
{
    public class Dash : BaseAbility
    {
        [Header("Dash")]
        [SerializeField] Sprite img;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TriggerAbility();
            }
        }
        public override void Ability()
        {
            StartCoroutine(InitiateIMG());
        }

        IEnumerator InitiateIMG()
        {
            int i = 0;
            while(i < 5)
            {
                GameObject obj = new GameObject("Img");
                SpriteRenderer objSprite = obj.AddComponent<SpriteRenderer>();
                objSprite.sprite = img;
                objSprite.color = Color.white * .5f;
                obj.transform.position = transform.position;
                i++;

                yield return new WaitForSeconds(.1f);
            }
        }
    }
}