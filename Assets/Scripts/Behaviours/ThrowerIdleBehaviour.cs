using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ThrowerIdleBehaviour : MonoBehaviour, IBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Execution()
    {
        _animator.SetBool("Idle", true);
        _animator.SetBool("Throw", false);
        _animator.SetBool("Attack", false);
    }

    public float Importance()
    {
        return 0.5f;
    }
}
