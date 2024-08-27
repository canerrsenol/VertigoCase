using KBCore.Refs;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, Anywhere] private GameObject bombPanel;
    [SerializeField, Anywhere] private IntEventChannelSO onSpinBomb;
    [SerializeField, Anywhere] private VoidEventChannelSO onGiveUpButtonClicked;
    [SerializeField, Anywhere] private VoidEventChannelSO onSpinReset;

    private void OnEnable()
    {
        onSpinBomb.OnEventRaised += ShowBombPanel;
        onGiveUpButtonClicked.OnEventRaised += HideBombPanel;
    }
    
    private void OnDisable()
    {
        onSpinBomb.OnEventRaised -= ShowBombPanel;
        onGiveUpButtonClicked.OnEventRaised -= HideBombPanel;
    }

    private void ShowBombPanel(int multiplier)
    {
        bombPanel.SetActive(true);
    }
    
    private void HideBombPanel()
    {
        onSpinReset.RaiseEvent();
        bombPanel.SetActive(false);
    }
}
