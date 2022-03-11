using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DoorAnimationController : MonoBehaviour
{
    private const string ParameterName = "Opened";
    private Animator _animator;

    public bool Opened { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        Opened = !Opened;
        _animator.SetBool(ParameterName, Opened);
    }
}