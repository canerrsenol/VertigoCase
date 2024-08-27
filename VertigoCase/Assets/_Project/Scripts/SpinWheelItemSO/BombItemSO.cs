using UnityEngine;

[CreateAssetMenu(fileName = "BombItem", menuName = "SpinWheel/BombItem")]
public class BombItemSO : SpinSliceItemSO
{
    public IntEventChannelSO bombEventChannel;
    public override void OnSpinWheelItem(int multiplier)
    {
        bombEventChannel.RaiseEvent(multiplier);
    }
}
