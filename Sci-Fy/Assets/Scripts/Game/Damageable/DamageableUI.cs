using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Game.Movement;

[RequireComponent(typeof(Damageable))]
public class DamageableUI : MonoBehaviour
{
    [SerializeField] private Canvas _hitCanvas;

    private TMP_Text _damageText;
    private Damageable _damageable;
    private GameObject[] _hitEffects = new GameObject[HIT_EFFECT_POOL_SIZE];
    private int _currentHitIndex = 0, lastHitIndex = 0;
    private float _lastDamageValue;

    #region OBJECT_POOL
    private static GameObject _damageableUIParent;
    public static GameObject DamageablueUIParent 
    {
        get
        {
            if(_damageableUIParent == null)
            {
                _damageableUIParent = new GameObject("Damageable UI Pool");
            }

            return _damageableUIParent;
        }   
    }

    const int HIT_EFFECT_POOL_SIZE = 10;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _damageable = GetComponent<Damageable>();
     
        _damageable.OnTakeDamage.AddListener(ActivateHitEffect);
        _damageable.OnDead.AddListener(ActivateHitEffect);

        InitializeHitDamageCanvas();
    }

    public void ActivateHitEffect(float damageReceived)
    {
        _hitEffects[lastHitIndex].SetActive(false);
        _hitEffects[_currentHitIndex].transform.position = transform.position;
        _hitEffects[_currentHitIndex].SetActive(true);

        _damageText = _hitEffects[_currentHitIndex].GetComponentInChildren<TMP_Text>();
        _damageText.text = $"{damageReceived}";

        //Set random color to the text
        _damageText.color = new Color(Random.Range(0f, 1f),
                                      Random.Range(0, 1f),
                                      Random.Range(0f, 1f)
                                     );

        //Using lerp movement to move the damage text
        var seq = LeanTween.sequence();
        seq.append(
            LeanTween.moveY(_hitEffects[_currentHitIndex], transform.position.y + 1f, 1f)
            );
        
        seq.append(() => 
        {
            _hitEffects[lastHitIndex].SetActive(false);
        });

        lastHitIndex = _currentHitIndex;
        _currentHitIndex = (_currentHitIndex + 1) % _hitEffects.Length;
    }

    private void DesactivateCurrentHitEffect() => _hitEffects[_currentHitIndex].SetActive(false);

    private void InitializeHitDamageCanvas()
    {
        for(int i = 0; i < HIT_EFFECT_POOL_SIZE; i++)
        {
            _hitEffects[i] = Instantiate(_hitCanvas.gameObject, transform.position, Quaternion.identity);
            _hitEffects[i].SetActive(false);
            _hitEffects[i].transform.SetParent(DamageablueUIParent.transform);
        }
    }
}
