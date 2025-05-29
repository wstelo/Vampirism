using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Health _health;

    private bool _isReady = false;
    private Vector3 _circlePosition;
    private float _circleRadius = 4;

    public event Action<float, float> ChangedCooldownTimerValue;
    public event Action<float, float> ChangedActionTimerValue;
    public event Action ActivatedAbility;
    public event Action DeactivatedAbility;

    public float ActionTime { get; private set; } = 6f;
    public float CooldownTime { get; private set; } = 4f;
    public float CurrentActionTime { get; private set; } = 0f;
    public float CurrentCooldownTime { get; private set; } = 0f;
    public bool IsActivate { get; private set; } = false;

    private void Start()
    {
        StartCoroutine(StartCooldown());
    }

    public void Update()
    {
        _circlePosition = transform.position;
    }

    public void FixedUpdate()
    {
        if (IsActivate)
        {
            _isReady = false;
            StartCoroutine (TakeOutHealth());
            IsActivate = false;
        }
    }

    public void Activate()
    {
        if (_isReady && IsActivate == false)
        {
            IsActivate = true;
        }
    }

    private IEnumerator TakeOutHealth()
    {
        float refreshTime = 0.25f;
        var wait = new WaitForSeconds(refreshTime);
        Collider2D currentTarget = null;
        float lastDistance = 0;
        float currentDistance;

        CurrentActionTime = 0f;
        ActivatedAbility?.Invoke();

        while (CurrentActionTime < ActionTime)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(_circlePosition, _circleRadius, _enemyLayer);

            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    if (currentTarget == null)
                    {
                        currentTarget = hit;
                        lastDistance = gameObject.transform.position.SqrDistance(hit.transform.position);
                    }

                    if(gameObject.transform.position.SqrDistance(hit.transform.position) < lastDistance)
                    {
                        currentTarget = hit;
                        lastDistance = gameObject.transform.position.SqrDistance(hit.transform.position);
                    }                                      
                }

                if(currentTarget != null && currentTarget.TryGetComponent(out Health enemy))
                {
                    enemy.TakeDamage(3);
                    _health.IncreaseHealth(3);
                }
            }

            CurrentActionTime += refreshTime;
            ChangedActionTimerValue?.Invoke(CurrentActionTime, ActionTime);

            yield return wait;
        }

        DeactivatedAbility?.Invoke();
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        CurrentCooldownTime = 0f;
        float refreshTime = 0.25f;
        var wait = new WaitForSeconds(refreshTime);

        while (CurrentCooldownTime < CooldownTime)
        {
            CurrentCooldownTime += refreshTime;
            ChangedCooldownTimerValue?.Invoke(CurrentCooldownTime, CooldownTime);

            yield return wait;
        }

        _isReady = true;
    }
}
