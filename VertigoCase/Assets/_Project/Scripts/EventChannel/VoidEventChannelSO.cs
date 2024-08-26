using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Events/VoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
    public event Action OnEventRaised;
    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
