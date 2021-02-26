using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Timer), typeof(AudioSource))]
public class LevelTimer : MonoBehaviour
{
    private AudioSource _source;
    private Timer _timer;
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _source = GetComponent<AudioSource>();

        _timer.OnSecondTick.AddListener(OnTenSecondsLeft);
    }

    private void OnTenSecondsLeft()
    {
        if(_timer.Seconds < 10f && _timer.Minutes == 0f)
        {
            _source.Play();
        }
    }
}
