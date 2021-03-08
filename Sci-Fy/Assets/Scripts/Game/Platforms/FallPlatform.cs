using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<Summary>
///A platform will fall when the method "Fall" is called
///</Summary>

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FallPlatform : MonoBehaviour
{
    private Rigidbody2D _rgb;
    private Collider2D _col;
    private Vector3 _initialPosition;


    [Header("Fall")]
    [SerializeField] private float _delayToFall;
    [SerializeField] private bool _fallWhenCollided;
    [SerializeField] private float _newPlatformGravityValue;

    [Header("Platform Return")]
    [SerializeField] private bool _returnToStartPosition;
    [SerializeField] private float _secondsToReturn;


    private void Start() {
        _rgb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();

        _initialPosition = transform.localPosition;
    }

    public IEnumerator MakePlatformFall(){
        yield return new WaitForSeconds(_delayToFall);

        _rgb.isKinematic = false;
        _rgb.gravityScale = _newPlatformGravityValue;

        if(_returnToStartPosition)
            StartCoroutine(ReturnPlatform());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(_fallWhenCollided && other.gameObject.CompareTag("Player"))
            StartCoroutine(MakePlatformFall());
    }

    private IEnumerator ReturnPlatform(){
        _col.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("No Ground");

        yield return new WaitForSeconds(_secondsToReturn);
        _rgb.gravityScale = 0f; 
        _rgb.isKinematic = true;
        _rgb.velocity = Vector2.zero;

        _col.isTrigger = false;

        transform.localPosition = _initialPosition;

        gameObject.layer = LayerMask.NameToLayer("Environment");
    }
}
