using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vampirism))]

public class AbilityEffects : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _effectAreaCircleSprite;

    private Vampirism _vampirism;

    private void Awake()
    {
        _vampirism = GetComponent<Vampirism>();
        _vampirism.ActivatedAbility += EnableEffect;
        _vampirism.DeactivatedAbility += DisableEffect;
        _effectAreaCircleSprite.enabled = false;
    }

    private void OnDisable()
    {
        _vampirism.ActivatedAbility -= EnableEffect;
        _vampirism.DeactivatedAbility -= DisableEffect;
    }

    private void EnableEffect()
    {
        _effectAreaCircleSprite.enabled = true;
    }

    private void DisableEffect()
    {
        _effectAreaCircleSprite.enabled = false;
    }
}
