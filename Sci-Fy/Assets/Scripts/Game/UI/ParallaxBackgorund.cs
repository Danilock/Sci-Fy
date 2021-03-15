using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace Game.UI{
public class ParallaxBackgorund : MonoBehaviour
    {
        [SerializeField] private RawImage _image;

        [Header("Parallax Speed")]
        [SerializeField] private float X;
        [SerializeField] private float Y;
        private Rect _copyRect = new Rect(0f, 0f, 1f, 1f);
        private Vector2 _rectPosition;
        private PlayerController _player;

        //Cvm
        private Camera _cvm;
        private Vector3 _cvmLastPosition;

        // Start is called before the first frame update
        void Start()
        {
            _cvm = Camera.main;
            _player = FindObjectOfType<PlayerController>();

            if(_image == null)
                _image = GetComponent<RawImage>();

            _cvmLastPosition = _cvm.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if(Vector3.Distance(_cvmLastPosition, _cvm.transform.position) > .083f)
                DoParallaxEffect();    
        }

        private void DoParallaxEffect(){
            _rectPosition += new Vector2(X * _player.Rigidbody.velocity.normalized.x, Y * _player.Rigidbody.velocity.normalized.y) / 2 * Time.deltaTime;

            _copyRect.position = _rectPosition;

            _image.uvRect = _copyRect;

            _cvmLastPosition = _cvm.transform.position;
        }
    }
}