using System;
using UnityEngine;

public class Flower : Item
{
    [SerializeField] private EEffectType effect;
    [SerializeField, Range(0.1f, 100)] private float effectValue;

    public EEffectType GetEffect() => effect;
    public float GetEffectValue() => effectValue;
}