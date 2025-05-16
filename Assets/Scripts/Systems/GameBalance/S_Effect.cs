using UnityEngine;

[System.Serializable]
public struct SEffect
{
    public EEffectType effect;
    public float value;

    public SEffect(EEffectType effect, float value)
    {
        this.effect = effect;
        this.value = Mathf.Clamp(0.1f, 100, value);
    }
}