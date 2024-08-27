using UnityEngine;

public abstract class SpinSliceItemSO : ScriptableObject
{
    public Sprite ItemSprite;
    public abstract void OnSpinWheelItem(int multiplier);
}
