using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ThrowBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private float _minDistanceThrowing;
    [SerializeField] private float _maxDistanceThrowing;
    [SerializeField] private float _importance;
    private Animator _animator;
    private PlayerComponent _target;

    private void Start()
    {
        _target = FindObjectOfType<PlayerComponent>();
        _animator = GetComponent<Animator>();
    }

    public float Importance()
    {
        
            float distance = (_target.gameObject.transform.position - gameObject.GetComponentInParent<Transform>().position).magnitude;
            if(distance >= _minDistanceThrowing && distance <= _maxDistanceThrowing)
            {
                return _importance;
            }
            return 0;
    }

    public void Execution()
    {
        _animator.SetBool("Throw", true);
        _animator.SetBool("Idle", false);
        _animator.SetBool("Attack", false);
    }
}
