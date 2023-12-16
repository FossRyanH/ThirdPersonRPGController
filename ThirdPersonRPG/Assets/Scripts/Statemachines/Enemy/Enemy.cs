using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState
{
    IDLE, PATROL, CHASE, ATTACK
}

public class Enemy : MonoBehaviour
{
    #region EnemyVariables
    Rigidbody _rb;
    EnemyState _state;
    NavMeshAgent _agent;
    GameObject _target = null;
    [SerializeField]
    Health _health;

    [SerializeField]
    EnemyDamageDealer _damageDealer;

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

    GameObject _player;

    [Header("Patrol Related")]
    [SerializeField]
    GameObject[] _patrolPoints;
    [SerializeField]
    int _currenntPatrolPoint = 0;
    [SerializeField]
    float _patrolRadius = 2f;

    [Header("Combat Related")]
    [SerializeField]
    float _combatRadius = 3f;
    [SerializeField]
    int _damageAmount = 10;
    [SerializeField]
    float _attackRadius = 1.5f;

    [Header("ItemDrop")]
    [SerializeField]
    GameObject[] _itemPrefab;
    int _randomItem;

    readonly int _locotmotionBlendTreeHash = Animator.StringToHash("Locomotion");
    readonly int _speedHash = Animator.StringToHash("MoveSpeed");
    readonly int _attackHash = Animator.StringToHash("Attack");
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        _player = GameObject.FindGameObjectWithTag("Player");

        _currentHealth = _maxHealth;
    }
    void Update()
    {
        UpdateAnimator();
        SetState();
    }

    void FixedUpdate()
    {
        AIAction();
    }

    void AIAction()
    {
        switch(_state)
        {
            case EnemyState.IDLE:
                _animator.CrossFade(_locotmotionBlendTreeHash, _crossFadeDuration);
                IdleState();
                break;
            case EnemyState.PATROL:
                _animator.CrossFade(_locotmotionBlendTreeHash, _crossFadeDuration);
                PatrolState();
                break;
            case EnemyState.ATTACK:
                AttackState();
                break;
            case EnemyState.CHASE:
                _animator.CrossFade(_locotmotionBlendTreeHash, _crossFadeDuration);
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
        }
        else
        {
            _state = EnemyState.PATROL;
        }

        if (IsInRange(_player, _combatRadius))
        {
            _target = _player;
            _state = EnemyState.ATTACK;
            _damageDealer.SetAttackStrength(_damageAmount);
        }
    }

    #region States
    void IdleState()
    {
        _agent.speed = 0f;
    }

    void ChaseState()
    {
        _agent.speed = 4.5f;

        ChangeTarget(_target);
    }

    void AttackState()
    {
        ChangeTarget(_target);
        _agent.speed = 1.5f;

        if (IsInRange(_target, _attackRadius))
        {
            _agent.speed = 0f;

            _animator.PlayInFixedTime(_attackHash);
        }

        if (GetNormalizedTime(_animator) >= 1f)
        {
            _state = EnemyState.CHASE;
        }

        FacePlayer();
    }

    void PatrolState()
    {
        _agent.speed = 2f;

        _target = _patrolPoints[_currenntPatrolPoint];
        ChangeTarget(_target);

        if (IsInRange(_target, _patrolRadius))
        {
            _currenntPatrolPoint++;
            _currenntPatrolPoint %= _patrolPoints.Length;

            ChangeTarget(_patrolPoints[_currenntPatrolPoint]);
        }
    }
    #endregion

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
                _animator.SetFloat(_speedHash, 0f, _animationDampingValue, Time.deltaTime);
                break;
            case EnemyState.PATROL:
                _animator.SetFloat(_speedHash, 0.5f, _animationDampingValue, Time.deltaTime);
                break;
            case EnemyState.CHASE:
                _animator.SetFloat(_speedHash, 1f, _animationDampingValue, Time.deltaTime);
                break;
            case EnemyState.ATTACK:
                break;
            default:
                break;
        }
    }

    void FacePlayer()
    {
        if (_target == null) { return; }
        Vector3 faceDirection = _target.transform.position - transform.position;
        faceDirection.y = 0f;

        transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    void OnDestroy()
    {
        _randomItem = Random.Range(0, _itemPrefab.Length);
        Instantiate(_itemPrefab[_randomItem], (transform.position + (Vector3.up * 1.25f)), Quaternion.identity);
    }
}
