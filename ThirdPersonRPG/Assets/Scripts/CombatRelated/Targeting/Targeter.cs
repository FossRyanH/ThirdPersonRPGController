using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField]
    CinemachineTargetGroup _cineTargetingGroup;
    List<Target> _targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

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
        if (_targets.Count == 0)
        {
            return false;
        }

        CurrentTarget = _targets[0];
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
