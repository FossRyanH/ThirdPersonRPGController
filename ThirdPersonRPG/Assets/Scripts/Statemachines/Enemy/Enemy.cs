using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState
{
    IDLE, PATROL, CHASE, ATTACK
}

public class Enemy : MonoBehaviour
{
    Rigidbody _rb;
    EnemyState _state;

    [Header("Animator Properties")]
    [SerializeField]
    Animator _animator;
    [SerializeField]
    float _crossFadeDuration = 0.15f;
    [SerializeField]
    float _animationDampingValue = 0.1f;

    [Header("Enemy Stats")]
    [SerializeField]
    int _maxHealth = 100;
    [SerializeField]
    int _currentHealth;

    [Header("Movement and Navigation")]
    [SerializeField]
    float _detectionRadius = 10f;

    NavMeshAgent _agent;

    GameObject _target = null;

    [SerializeField]
    GameObject _player;

    [Header("Patrol Related")]
    [SerializeField]
    GameObject[] _patrolPoints;
    [SerializeField]
    int _currenntPatrolPoint = 0;
    [SerializeField]
    float _patrolRadius = 2f;

    readonly int LocotmotionBlendTreeHash = Animator.StringToHash("Locomotion");
    readonly int SpeedHash = Animator.StringToHash("MoveSpeed");

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        if (_player == null)
        {
            Debug.Log("Player not Found");
        }

        _currentHealth = _maxHealth;
    }
    void Update()
    {
        UpdateAnimator();
        SetState();
        AIAction();
    }

    void AIAction()
    {
        switch(_state)
        {
            case EnemyState.IDLE:
                _animator.CrossFade(LocotmotionBlendTreeHash, _crossFadeDuration);
                IdleState();
                break;
            case EnemyState.PATROL:
                _animator.CrossFade(LocotmotionBlendTreeHash, _crossFadeDuration);
                PatrolState();
                break;
            case EnemyState.ATTACK:
                AttackState();
                break;
            case EnemyState.CHASE:
                _animator.CrossFade(LocotmotionBlendTreeHash, _crossFadeDuration);
                ChaseState();
                break;
            default:
                break;
        }
    }

    void SetState()
    {
        if (IsInRange(_player, _detectionRadius))
        {
            _target = _player;
            _state = EnemyState.CHASE;
            _agent.speed = 4f;
        }
        else
        {
            _target = null;
            _state = EnemyState.PATROL;
            _agent.speed = 2f;
        }
    }

    void IdleState()
    {
        Debug.Log("Idling");
    }

    void ChaseState()
    {
        Debug.Log("Chasing");
        ChangeTarget(_target);
    }

    void AttackState()
    {
        Debug.Log("Attacking");
    }

    void PatrolState()
    {
        Debug.Log("Patrolling");

        _target = _patrolPoints[_currenntPatrolPoint];
        ChangeTarget(_target);

        if (IsInRange(_target, _patrolRadius))
        {
            _currenntPatrolPoint++;
            _currenntPatrolPoint %= _patrolPoints.Length;

            ChangeTarget(_patrolPoints[_currenntPatrolPoint]);
        }
    }

    bool IsInRange(GameObject target, float distanceCheck)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        return distance < distanceCheck;
    }

    void ChangeTarget(GameObject target)
    {
        _agent.destination = target.transform.position;
    }

    void UpdateAnimator()
    {
        switch(_state)
        {
            case EnemyState.IDLE:
                _animator.SetFloat(SpeedHash, 0f, _animationDampingValue, Time.deltaTime);
                break;
            case EnemyState.PATROL:
                _animator.SetFloat(SpeedHash, 0.5f, _animationDampingValue, Time.deltaTime);
                break;
            case EnemyState.CHASE:
                _animator.SetFloat(SpeedHash, 1f, _animationDampingValue, Time.deltaTime);
                break;
            default:
                break;
        }
    }
}
