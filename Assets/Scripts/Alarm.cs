using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _fadeSpeed = 0.00030f;

    private const float MinVolume = 0.0f;
    private const float MaxVolume = 1.0f;

    private bool _coroutineIsRunning = false;
    private AudioSource _alarmSystem;
    private Collider _collider;
    private Coroutine _alarmSystemJob;

    private void Awake()
    {
        _alarmSystem = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        _coroutineIsRunning = true;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarmSystem.Play();

            _alarmSystemJob = StartCoroutine(FadeVolume());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        _coroutineIsRunning = false;

        StopCoroutine(_alarmSystemJob);

        _alarmSystem.Stop();
    }

    private IEnumerator FadeVolume()
    {
        while (_coroutineIsRunning)
        {
            var volume = Mathf.MoveTowards(MinVolume, MaxVolume, _fadeSpeed);

            for (_alarmSystem.volume = MinVolume; _alarmSystem.volume < MaxVolume; _alarmSystem.volume += volume)
            {
                yield return null;
            }

            for (_alarmSystem.volume = MaxVolume; _alarmSystem.volume > 0; _alarmSystem.volume -= volume)
            {
                yield return null;
            }
        }
    }
}