using UnityEngine;

[CreateAssetMenu(fileName = "SpinWheelVisual", menuName = "SpinWheelVisual/SpinWheelVisual")]
public class SpinWheelZoneSettingsSO : ScriptableObject
{
    public Sprite SpinSprite;
    public Sprite IndicatorSprite;
    public string Name;
    public string ItemDescription;
    public bool isRiskFree;
    public int zoneMultiplier;
    public SpinSliceItemSO[] canHaveItems;
}
