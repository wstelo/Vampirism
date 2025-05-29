using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;

    public event Action AttackEnded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartIdleAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsIdle, true);
    }

    public void StartRunAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsRun, true);
    }

    public void StartJumpAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsJump, true);
    }

    public void StartIdleAttackAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsIdleAttack, true);
    }

    public void StartRunAttackAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsRunAttack, true);
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsIdle, false);
    }

    public void StopRunAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsRun, false);
    }

    public void StopJumpAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsJump, false);
    }

    public void StopIdleAttackAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsIdleAttack, false);
    }

    public void StopRunAttackAnimation()
    {
        _animator.SetBool(PlayerAnimationData.IsRunAttack, false);
    }

    public void AttackAnimationEnded()
    {
        AttackEnded?.Invoke();
    }
}
