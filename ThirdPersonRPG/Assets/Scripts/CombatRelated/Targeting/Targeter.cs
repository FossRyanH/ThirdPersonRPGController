using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField]
    CinemachineTargetGroup _cineTargetingGroup;
    List<Target> _targets = new List<Target>();

    Camera _mainCamera;

    public Target CurrentTarget { get; private set; }

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
         _targets.Add(target);
         target.DeathEvent += RemoveTarget;
    }

    void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;
        if (_targets.Count == 0)
        {
            return false;
        }

        foreach(Target target in _targets)
        {
            Vector2 viewPosition = _mainCamera.WorldToViewportPoint(target.transform.position);

            if (viewPosition.x < 0f || viewPosition.x > 1f || viewPosition.y < 0f || viewPosition.y > 1f)
            {
                continue;
            }

            Vector2 centerScreen = viewPosition - new Vector2(0.5f, 0.5f);
            if (centerScreen.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = centerScreen.sqrMagnitude;
            }
        }

        if (closestTarget == null)
        {
            return false;
        }

        CurrentTarget = closestTarget;
        _cineTargetingGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void CancelTarget()
    {
        if (CurrentTarget == null)
        {
            return;
        }

        _cineTargetingGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            _cineTargetingGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.DeathEvent -= RemoveTarget;
        _targets.Remove(target);
    }
}
