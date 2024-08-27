using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelVisual : ValidatedMonoBehaviour
{
    [SerializeField, Anywhere] private Image spinWheelImage;
    [SerializeField, Anywhere] private Image indicatorImage;
    [SerializeField, Anywhere] private TextMeshProUGUI spinWheelNameText;
    [SerializeField, Anywhere] private TextMeshProUGUI spinWheelDescriptionText;
    
    public void SetSpinWheelVisual(SpinWheelZoneSettingsSO spinWheelItem)
    {
        spinWheelImage.sprite = spinWheelItem.SpinSprite;
        indicatorImage.sprite = spinWheelItem.IndicatorSprite;
        spinWheelNameText.text = spinWheelItem.Name;
        spinWheelDescriptionText.text = spinWheelItem.ItemDescription;
    }
}
