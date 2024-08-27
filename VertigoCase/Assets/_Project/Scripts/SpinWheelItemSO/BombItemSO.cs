using UnityEngine;

[CreateAssetMenu(fileName = "BombItem", menuName = "SpinWheel/BombItem")]
public class BombItemSO : SpinWheelItemSO
{
    public IntEventChannelSO bombEventChannel;
    public override void OnSpinWheelItem(int multiplier)
    {
        bombEventChannel.RaiseEvent(multiplier);
        Debug.Log("Bomb Item");
    }
}
