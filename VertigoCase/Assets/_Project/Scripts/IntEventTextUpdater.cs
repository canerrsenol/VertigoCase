using DG.Tweening;
using KBCore.Refs;
using TMPro;
using UnityEngine;

public class IntEventTextUpdater : ValidatedMonoBehaviour
{
    [SerializeField, Child] private TextMeshProUGUI text;
    [SerializeField, Anywhere] private IntEventChannelSO intEventChannelSO;
    
    private int currentValue;

    private void OnEnable()
    {
        intEventChannelSO.OnEventRaised += UpdateText;
    }

    private void OnDisable()
    {
        intEventChannelSO.OnEventRaised -= UpdateText;
    }

    private void UpdateText(int value)
    {
        DOVirtual.Int(currentValue, value, 1f, (x) =>
            {
                currentValue = x;
                text.text = currentValue.ToString();
            });
        text.gameObject.name = text.name + "_" + value;
    }
}
