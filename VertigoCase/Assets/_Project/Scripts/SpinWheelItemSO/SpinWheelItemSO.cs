using UnityEngine;

public abstract class SpinWheelItemSO : ScriptableObject
{
    public Sprite ItemSprite;
    public abstract void OnSpinWheelItem(int multiplier);
}
