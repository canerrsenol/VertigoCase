using UnityEngine;

[CreateAssetMenu(fileName = "GoldItem", menuName = "SpinWheel/GoldItem")]
public class GoldItemSO : SpinWheelItemSO
{
    public IntEventChannelSO OnGoldItemCollected;
    public override void OnSpinWheelItem(int multiplier)
    {
        OnGoldItemCollected.RaiseEvent(multiplier);
    }
}
