using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinSlice : MonoBehaviour
{
    [SerializeField, Child] private TextMeshProUGUI multiplierText;
    [SerializeField, Child] private Image itemImage;

    [SerializeField, Anywhere] private SpinSliceItemSO spinSliceItem;
    [SerializeField] private int slotMultiplier = 10;

    private void OnValidate()
    {
        UpdateSliceVisual();
    }
    
    public void SpinSliceAction()
    {
        spinSliceItem.OnSpinWheelItem(slotMultiplier);
    }
    
    public void SetItem(SpinSliceItemSO item, int multiplier)
    {
        spinSliceItem = item;
        slotMultiplier = multiplier;
        UpdateSliceVisual();
    }

    private void UpdateSliceVisual()
    {
        multiplierText.text = slotMultiplier + "x";
        multiplierText.gameObject.name = multiplierText.name + "_" + slotMultiplier;

        if (spinSliceItem != null)
        {
            itemImage.sprite = spinSliceItem.ItemSprite;
            itemImage.gameObject.name = itemImage.name + "_" + spinSliceItem.name;
        }
    } 
}
