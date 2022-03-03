using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FadeUp))]

public class Door : MonoBehaviour
{
    private Animator _animator;
    private string _parameterName = "Opened";
    private FadeUp _fadeUp;

    public bool Opened { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fadeUp = GetComponent<FadeUp>();
    }

    public void PlayAnimation()
    {
        Opened = !Opened;
        _animator.SetBool(_parameterName, Opened);
    }

    public void StartFadeUp()
    {
        _fadeUp.StartChangeVolume();
    }
}
