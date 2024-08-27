using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventListener : MonoBehaviour
{
    [SerializeField, Anywhere] private VoidEventChannelSO buttonEventChannelSO;
    
    [SerializeField, Self] private Button button;
    
    private void OnEnable()
    {
        button.onClick.AddListener(ButtonAction);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(ButtonAction);
    }
    
    private void ButtonAction()
    {
        buttonEventChannelSO.RaiseEvent();
    }
}
