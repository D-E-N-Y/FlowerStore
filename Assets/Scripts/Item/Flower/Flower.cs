using System;

public class Flower : Item
{
    public Action applyEffect;

    public void ApplyEffent()
    {
        applyEffect?.Invoke();
    }
}