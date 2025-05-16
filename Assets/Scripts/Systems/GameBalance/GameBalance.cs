using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBalance : MonoBehaviour
{
    public static GameBalance current;
    public Action updateData;

    private Dictionary<EEffectType, float> effects;

    public void Initiailize()
    {
        effects = new Dictionary<EEffectType, float>();

        foreach (EEffectType effect in Enum.GetValues(typeof(EEffectType)))
        {
            effects[effect] = 0f;
        }

        current = this;
    }

    public void ApplyEffect(EEffectType effect, float value)
    {
        effects[effect] += value;
        updateData?.Invoke();
    }

    public void CancelEffect(EEffectType effect, float value)
    {
        effects[effect] = Math.Max(effects[effect] - value, 0);
        updateData?.Invoke();
    }

    public float GetEffect(EEffectType effect) => effects[effect];
}
