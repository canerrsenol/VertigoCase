using UnityEngine;

[CreateAssetMenu(fileName = "BombItem", menuName = "SpinWheel/BombItem")]
public class BombItemSO : SpinWheelItemSO
{
    public VoidEventChannelSO bombEventChannel;
    public override void OnSpinWheelItem()
    {
        bombEventChannel.RaiseEvent();
        Debug.Log("Bomb Item");
    }
}
