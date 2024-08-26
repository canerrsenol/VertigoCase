using UnityEngine;

[CreateAssetMenu(fileName = "MoneyItem", menuName = "SpinWheel/MoneyItem")]
public class MoneyItemSO : SpinWheelItemSO
{
    public VoidEventChannelSO moneyEventChannel;
    public override void OnSpinWheelItem()
    {
        moneyEventChannel.RaiseEvent();
        Debug.Log("Money Item");
    }
}
