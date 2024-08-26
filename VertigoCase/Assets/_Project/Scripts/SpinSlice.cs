using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinSlice : MonoBehaviour
{
    [SerializeField, Child] private TextMeshProUGUI multiplierText;
    [SerializeField, Child] private Image itemImage;

    [SerializeField, Anywhere] private SpinWheelItemSO spinSliceItem;
    [SerializeField] private int slotMultiplier = 10;

    private void OnValidate()
    {
        multiplierText.text = slotMultiplier + "x";
        itemImage.sprite = spinSliceItem?.ItemSprite;
    }
    
    public void SpinSliceAction()
    {
        spinSliceItem.OnSpinWheelItem();
    }
}
