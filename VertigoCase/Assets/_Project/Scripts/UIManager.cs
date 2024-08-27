using KBCore.Refs;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, Anywhere] private GameObject bombPanel;
    [SerializeField, Anywhere] private IntEventChannelSO onSpinBomb;
    [SerializeField, Anywhere] private VoidEventChannelSO onGiveUpButtonClicked;

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
        bombPanel.SetActive(false);
    }
}
