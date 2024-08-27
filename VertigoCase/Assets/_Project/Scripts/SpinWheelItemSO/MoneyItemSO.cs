using UnityEngine;

[CreateAssetMenu(fileName = "MoneyItem", menuName = "SpinWheel/MoneyItem")]
public class MoneyItemSO : SpinWheelItemSO
{
    public IntEventChannelSO moneyEventChannel;
    public override void OnSpinWheelItem(int multiplier)
    {
        moneyEventChannel.RaiseEvent(multiplier);
    }
}
