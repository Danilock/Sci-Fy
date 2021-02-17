using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float _startMana;
    [SerializeField] private float _currentMana;
    [SerializeField, Range(1, 10)] private float _manaRegenerationRate;
    public float CurrentMana 
    {
        get
        {
            return _currentMana;
        } 

        private set 
        {
            _currentMana = value;
        } 
    }
    private float _maxMana;
    public float MaxMana 
    { 
        get
        {
            return _maxMana;
        }
        private set
        {
            _maxMana = value;
        }
    }
    private void Start()
    {
        CurrentMana = _startMana;
        MaxMana = _startMana;
    }

    public void AddMana(float amount)
    {
        float total = CurrentMana + amount;

        //Avoiding CurrentMana exceding maxValue
        if (total <= MaxMana)
            CurrentMana = total;
        else
            CurrentMana = _maxMana;
    }

    public void ChangeMaxMana(float amount) => MaxMana = amount;

    public void RestMana(float amount)
    {
        if (CurrentMana <= 0f || amount > CurrentMana)
            return;
        CurrentMana -= amount;
        StartCoroutine(RenegerateMana());
    }

    IEnumerator RenegerateMana()
    {
        while(CurrentMana < MaxMana)
        {
            CurrentMana += .01f;
            yield return null;
        }
    }
}
