using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ThrowerAttackBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private int _importance;
    [SerializeField] private float _maxDistanceAttack;
    private Animator _animator;
    private PlayerComponent _target;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _target = FindObjectOfType<PlayerComponent>();
    }

    public void Execution()
    {
        
            _animator.SetBool("Idle", false);
            _animator.SetBool("Throw", false);
            _animator.SetBool("Attack", true);
    }

    public float Importance()
    {
        float distance = (_target.transform.position - transform.position).magnitude;
        if(distance <= _maxDistanceAttack)
        {
            return _importance;
        }
        return 0;
    }
}
