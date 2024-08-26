using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinChestItemChannel", menuName = "Events/SpinChestItemChannel")]
public class ChestItemChannelSO : ScriptableObject
{
    public event Action<ChestType> OnEventRaised;
    public void RaiseEvent(ChestType chestType)
    {
        OnEventRaised?.Invoke(chestType);
    }
}
