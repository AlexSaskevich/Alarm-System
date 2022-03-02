using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _maxDistance = 5.0f;
    [SerializeField] private AudioSource _alarmSystem;

    private bool _opened = false;
    private Animator _animator;
    private float _currentVolume = 0.0f;
    private float _maxVolume = 1.0f;
    private float _fadeSpeed = 0.00030f;
    private Coroutine _coroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Open();
        }
    }

    private void StartFadeUp()
    {
        if (_opened == true)
        {
            _alarmSystem.Play();
            _coroutine = StartCoroutine(FadeUp());
        }
        else
        {
            StopCoroutine(_coroutine);
            _alarmSystem.Stop();
            _currentVolume = 0f;
            _alarmSystem.volume = _currentVolume;
        }
    }

    private void Open()
    {
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out RaycastHit hit, _maxDistance) == false)
        {
            return;
        }

        if (hit.collider.CompareTag("Door"))
        {
            _animator = hit.collider.GetComponentInParent<Animator>();

            _opened = !_opened;

            _animator.SetBool("Opened", _opened);

            StartFadeUp();
        }
    }

    private IEnumerator FadeUp()
    {
        while (_alarmSystem.volume != _maxVolume)
        {
            _alarmSystem.volume += Mathf.MoveTowards(_currentVolume, _maxVolume, _fadeSpeed);

            yield return null;
        }
    }
}