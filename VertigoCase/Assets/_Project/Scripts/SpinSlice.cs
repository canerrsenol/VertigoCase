using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinSlice : MonoBehaviour
{
    [SerializeField, Child] private TextMeshProUGUI multiplierText;
    [SerializeField, Child] private Image itemImage;

    [SerializeField, Anywhere] private SpinWheelItemSO spinWheelItem;
    [SerializeField] private int slotMultiplier = 10;

    private void OnValidate()
    {
        multiplierText.text = slotMultiplier + "x";
        itemImage.sprite = spinWheelItem?.ItemSprite;
    }
}
