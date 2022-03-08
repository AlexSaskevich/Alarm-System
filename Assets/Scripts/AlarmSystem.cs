using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _currentVolume = 0.0f;
    [SerializeField] private float _fadeSpeed = 0.00030f;

    private float _maxVolume = 1.0f;
    private AudioSource _alarmSystem;
    private bool isExit;
    private Collider _collider;

    private void Awake()
    {
        _alarmSystem = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        isExit = false;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarmSystem.Play();
            StartCoroutine(ChangeVolume());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        isExit = true;

        StopAllCoroutines();

        _alarmSystem.Stop();
        _currentVolume = 0.0f;
        _alarmSystem.volume = _currentVolume;
    }

    private IEnumerator TurnUpVolume()
    {
        while (_alarmSystem.volume != _maxVolume)
        {
            _alarmSystem.volume += Mathf.MoveTowards(_currentVolume, _maxVolume, _fadeSpeed);

            yield return null;
        }
    }

    private IEnumerator TurnDownVolume()
    {
        while (_alarmSystem.volume != 0.0f)
        {
            _alarmSystem.volume -= Mathf.MoveTowards(_currentVolume, _maxVolume, _fadeSpeed);

            yield return null;
        }
    }

    private IEnumerator ChangeVolume()
    {
        while (isExit == false)
        {
            yield return StartCoroutine(TurnUpVolume());

            yield return new WaitForSeconds(1);

            yield return StartCoroutine(TurnDownVolume());
        }
    }
}