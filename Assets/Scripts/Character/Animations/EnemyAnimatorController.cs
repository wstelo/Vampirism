using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator _animator;

    public event Action AttackEnded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartIdleAnimation()
    {
        _animator.SetBool(EnemyAnimationData.IsIdle, true);
    }

    public void StartRunAnimation()
    {
        _animator.SetBool(EnemyAnimationData.IsRun, true);
    }

    public void StartAttackAnimation()
    {
        _animator.SetBool(EnemyAnimationData.IsAttack, true);
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(EnemyAnimationData.IsIdle, false);
    }

    public void StopRunAnimation()
    {
        _animator.SetBool(EnemyAnimationData.IsRun, false);
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool(EnemyAnimationData.IsAttack, false);
    }

    public void AttackAnimationEnded()
    {
        AttackEnded?.Invoke();
    }
}
