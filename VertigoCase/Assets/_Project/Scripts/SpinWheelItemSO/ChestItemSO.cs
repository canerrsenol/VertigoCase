using UnityEngine;

[CreateAssetMenu(fileName = "ChestItem", menuName = "SpinWheel/ChestItem")]
public class ChestItemSO : SpinWheelItemSO
{
    public ChestType chestType;
    
    public ChestItemChannelSO chestItemChannel;
    public override void OnSpinWheelItem(int multiplier)
    {
        chestItemChannel.RaiseEvent(chestType, multiplier);
    }
}