using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WarriorAttackBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private float _maxAttackDistance;
    [SerializeField] private int _importance;
    private WarriorComponent _warriorComponent;
    private Animator _animator;
    private PlayerComponent _target;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _target = FindObjectOfType<PlayerComponent>();
        _warriorComponent = GetComponentInParent<WarriorComponent>();
    }

    public void Execution()
    {
        _animator.SetBool("Attack", true);
        _warriorComponent.Attack = true;
    }

    public float Importance()
    {
        float distance = (_target.transform.position - transform.position).magnitude;
        if (distance <= _maxAttackDistance)
        {
            return _importance;
        }
        return 0;
    }
}
