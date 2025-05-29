using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.ValueChanged += ChangeValue;
    }

    private void OnDisable()
    {
        Health.ValueChanged -= ChangeValue;
    }

    public virtual void ChangeValue(float value)
    {

    }
}
