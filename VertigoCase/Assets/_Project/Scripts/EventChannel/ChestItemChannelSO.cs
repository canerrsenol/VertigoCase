using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinChestItemChannel", menuName = "Events/SpinChestItemChannel")]
public class ChestItemChannelSO : ScriptableObject
{
    public event Action<ChestType, int> OnEventRaised;
    public void RaiseEvent(ChestType chestType, int multiplier)
    {
        OnEventRaised?.Invoke(chestType, multiplier);
    }
}
