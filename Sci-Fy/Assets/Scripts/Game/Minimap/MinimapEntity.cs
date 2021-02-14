using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Minimap
{
    public class MinimapEntity : MonoBehaviour
    {
        [SerializeField] private Sprite _icon; //Icon to show on minimap
        [SerializeField] private Color _color; 
        private SpriteRenderer _iconRenderer;
        private GameObject _iconObj;

        private static GameObject _minimapContainer;
        public static GameObject MinimapContainer{
            get{
                if(_minimapContainer == null){
                    _minimapContainer = new GameObject("Minimap Icons Conatiner");
                }

                return _minimapContainer;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            InstantiateIconOnMinimap();
        }

        // Update is called once per frame
        void Update()
        {
            _iconObj.transform.position = transform.position;
        }
        
        private void InstantiateIconOnMinimap(){
            _iconObj = new GameObject(gameObject.name + " minimap icon");
            _iconObj.transform.localScale = transform.localScale;

            _iconObj.layer = LayerMask.NameToLayer("Minimap Entity");
            _iconObj.transform.SetParent(MinimapContainer.transform);

            _iconRenderer = _iconObj.AddComponent<SpriteRenderer>();
            _iconRenderer.sprite = _icon;
            _iconRenderer.color = _color;
        }
    }
}