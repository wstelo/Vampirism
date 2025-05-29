using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxValue = 100;

    private float _minValue = 0;

    public event Action<Health> Ended;
    public event Action<float> ValueChanged;
    public float CurrentValue { get; private set; }
    public float MaxValue => _maxValue;

    public void Awake()
    {
        CurrentValue = _maxValue;
    }

    public void IncreaseValue(float count)
    {
        if (count > 0)
        {
            CurrentValue += count;
            CurrentValue = Mathf.Clamp(CurrentValue, _minValue, _maxValue);
            ValueChanged?.Invoke(CurrentValue);
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            CurrentValue -= damage;

            if (CurrentValue <= 0)
            {
                CurrentValue = 0;
                Ended?.Invoke(this);
            }

            ValueChanged?.Invoke(CurrentValue);
        }
    }
}
