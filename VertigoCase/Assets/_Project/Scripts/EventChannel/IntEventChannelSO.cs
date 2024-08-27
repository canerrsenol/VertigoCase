using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Events/IntEventChannel")]
public class IntEventChannelSO : ScriptableObject
{
    public event Action<int> OnEventRaised;
    
    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
