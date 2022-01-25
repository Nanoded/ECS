using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WarriorRunBehaviour : MonoBehaviour, IBehaviour
{
    private WarriorComponent _warriorComponent;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _warriorComponent = GetComponentInParent<WarriorComponent>();
    }

    public void Execution()
    {
        _animator.SetBool("Attack", false);
        _warriorComponent.Attack = false;
    }

    public float Importance()
    {
        return 0.5f;
    }
}
