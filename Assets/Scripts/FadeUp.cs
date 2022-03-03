using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Door))]

public class FadeUp : MonoBehaviour
{
    [SerializeField] private float _currentVolume = 0.0f;
    [SerializeField] private float _fadeSpeed = 0.00030f;

    private float _maxVolume = 1.0f;
    private AudioSource _alarmSystem;
    private Door _door;
    private Coroutine _coroutine;

    private void Awake()
    {
        _alarmSystem = GetComponent<AudioSource>();
        _door = GetComponent<Door>();
    }

    public void StartChangeVolume()
    {
        if (_door.Opened == true)
        {
            _alarmSystem.Play();
            _coroutine = StartCoroutine(ChangeVolume());
        }
        else
        {
            StopCoroutine(_coroutine);
            _alarmSystem.Stop();
            _currentVolume = 0.0f;
            _alarmSystem.volume = _currentVolume;
        }
    }

    private IEnumerator ChangeVolume()
    {
        while (_alarmSystem.volume != _maxVolume)
        {
            _alarmSystem.volume += Mathf.MoveTowards(_currentVolume, _maxVolume, _fadeSpeed);

            yield return null;
        }
    }
}