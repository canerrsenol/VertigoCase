using UnityEngine;

[CreateAssetMenu(fileName = "GoldItem", menuName = "SpinWheel/GoldItem")]
public class GoldItemSO : SpinWheelItemSO
{
    public VoidEventChannelSO OnGoldItemCollected;
    public override void OnSpinWheelItem()
    {
        OnGoldItemCollected.RaiseEvent();
    }
}
