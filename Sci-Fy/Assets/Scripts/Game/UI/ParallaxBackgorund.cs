using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            _rectPosition += new Vector2(X * _player.Rigidbody.velocity.normalized.x, Y * _player.Rigidbody.velocity.normalized.y) / 2 * Time.deltaTime;

            _copyRect.position = _rectPosition;

            _image.uvRect = _copyRect;
        }
    }
}