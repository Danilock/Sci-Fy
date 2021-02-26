using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _minutes;
    [SerializeField] private float _seconds;
    public UnityEvent OnTimerEnd;
    public UnityEvent OnSecondTick;//Event is called every second
    [SerializeField, Range(1, 2)] private float _timerSpeedModifier = 1;
    bool isTicking = false;

    public float Minutes { get => _minutes; }
    public float Seconds { get => _seconds; }

    private TimerState _currentTimerState;

    // Update is called once per frame
    void Update()
    {
        if (_currentTimerState == TimerState.Ticking)
        {
            TickTimer();
        }
    }

    private void TickTimer()
    {
        _seconds -= 1 * Time.deltaTime * _timerSpeedModifier;
        if (!isTicking)
            StartCoroutine(OnTick());

        if(_seconds <= 0f)
        {
            if(_minutes <= 0f)
            {
                OnTimerEnd.Invoke();
                _currentTimerState = TimerState.Stopped;
            }
            else
            {
                _minutes -= 1;
                _seconds = 60;
            }
        }
    }

    public void StartTicking() => _currentTimerState = TimerState.Ticking;
    public void StartTicking(float seconds)
    {
        CalculateMinutesBasedOnSeconds(seconds);

        _currentTimerState = TimerState.Ticking;
    }

    private void CalculateMinutesBasedOnSeconds(float seconds)
    {
        if (seconds > 60f)
        {
            _minutes = (int)seconds / 60;
            _seconds = seconds % (_minutes * 60);
        }
        else
            _seconds = seconds;
    }
    
    private IEnumerator OnTick()
    {
        isTicking = true;
        yield return new WaitForSeconds(1f);
        OnSecondTick.Invoke();
        isTicking = false;
    }
}

public enum TimerState
{
    Stopped,
    Ticking
}
