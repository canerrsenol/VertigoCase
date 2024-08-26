using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Events/BoolEventChannel")]
public class BoolEventChannel : ScriptableObject
{
    public event Action<bool> OnEventRaised;
    public void RaiseEvent(bool value)
    {
        OnEventRaised?.Invoke(value);
    }
}
