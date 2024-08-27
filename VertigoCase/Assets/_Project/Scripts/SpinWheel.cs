using UnityEngine;
using DG.Tweening;
using KBCore.Refs;

public class SpinWheel : ValidatedMonoBehaviour
{
    [SerializeField, Self] private SpinWheelVisual spinWheelVisual;
    [SerializeField] private float spinDuration;
    [SerializeField] private float baseSpinAngle;
    [SerializeField] private AnimationCurve spinCurve;
    
    [SerializeField, Anywhere] SpinSliceItemSO bombItem;
    
    [SerializeField, Anywhere] SpinWheelZoneSettingsSO bronzeZoneSettings;
    [SerializeField, Anywhere] SpinWheelZoneSettingsSO silverZoneSettings;
    [SerializeField, Anywhere] SpinWheelZoneSettingsSO goldZoneSettings;
    
    [SerializeField, Anywhere] private IntEventChannelSO onZoneCounterChanged;
    [SerializeField, Anywhere] private VoidEventChannelSO onSpinWheelClicked;
    [SerializeField, Anywhere] private VoidEventChannelSO onSpinReset;
    
    // For indicator animation
    [SerializeField, Anywhere] private Transform indicatorTransform;
    private float lastCheckedAngle;
    
    private float spinAngleForSlice;
    private int currentSliceIndex;
    private bool isSpinning;
    private int zoneCounter;
    
    private SpinWheelZoneSettingsSO currentZoneSettings;

    [SerializeField, Child] private SpinSlice[] spinSlices;
    
    private void Start()
    {
        currentZoneSettings = bronzeZoneSettings;
        spinWheelVisual.SetSpinWheelVisual(bronzeZoneSettings);
        
        spinAngleForSlice = 360f / spinSlices.Length;
        zoneCounter = 1;
        onZoneCounterChanged.RaiseEvent(zoneCounter);
    }
    
    private void OnEnable()
    {
        onSpinWheelClicked.OnEventRaised += Spin;
        onSpinReset.OnEventRaised += ResetSpin;
    }

    private void OnDisable()
    {
        onSpinWheelClicked.OnEventRaised -= Spin;
        onSpinReset.OnEventRaised -= ResetSpin;
    }

    private void ResetSpin()
    {
        zoneCounter = 1;
        onZoneCounterChanged.RaiseEvent(zoneCounter);
        currentZoneSettings = bronzeZoneSettings;
        spinWheelVisual.SetSpinWheelVisual(bronzeZoneSettings);
        UpdateSliceItems(currentZoneSettings);
    }

    public void Spin()
    {
        if (isSpinning) { return; }
        
        int randomSlotIndex = Random.Range(0, spinSlices.Length);
        float angleToSpin = randomSlotIndex * spinAngleForSlice;
        
        isSpinning = true;
        transform.DORotate(new Vector3(0, 0, baseSpinAngle + angleToSpin), spinDuration, RotateMode.FastBeyond360)
            .SetEase(spinCurve)
            .OnUpdate(CheckIndicatorAnimation)
            .OnComplete(() =>
            {
                currentSliceIndex = randomSlotIndex;
                spinSlices[currentSliceIndex].SpinSliceAction();
                
                DOVirtual.DelayedCall(1f, () => 
                {
                    isSpinning = false;
                    PrepareForNextSpin();
                });
                
                Debug.Log("Spin completed at index: " + currentSliceIndex);
            });
    }

    private void PrepareForNextSpin()
    {
        // Update zone counter
        zoneCounter++;
        onZoneCounterChanged.RaiseEvent(zoneCounter);

        if (zoneCounter == 5)
        {
            currentZoneSettings = silverZoneSettings;
        }
        else if (zoneCounter == 30)
        {
            currentZoneSettings = goldZoneSettings;
        }
        else
        {
            currentZoneSettings = bronzeZoneSettings;
        }
        
        spinWheelVisual.SetSpinWheelVisual(currentZoneSettings);
        UpdateSliceItems(currentZoneSettings);
    }

    private void UpdateSliceItems(SpinWheelZoneSettingsSO zoneSettings)
    {
        if (zoneSettings.isRiskFree)
        {
            for (int i = 0; i < spinSlices.Length; i++)
            {
                SetRandomItemToSlice(zoneSettings, i);
            }
        }
        else
        {
            int bombIndex = Random.Range(0, spinSlices.Length);
            spinSlices[bombIndex].SetItem(bombItem, 1);
            
            for (int i = 0; i < spinSlices.Length; i++)
            {
                if (i == bombIndex) { continue; }
                SetRandomItemToSlice(zoneSettings, i);
            }
        }
    }

    private void SetRandomItemToSlice(SpinWheelZoneSettingsSO zoneSettings, int i)
    {
        var randomItem = zoneSettings.canHaveItems[Random.Range(0, zoneSettings.canHaveItems.Length)];
        spinSlices[i].SetItem(randomItem, zoneSettings.zoneMultiplier * zoneCounter);
    }

    private void CheckIndicatorAnimation()
    {
        float currentAngle = transform.eulerAngles.z;
        if (Mathf.Abs(currentAngle - lastCheckedAngle) >= 30f)
        {
            indicatorTransform.DOKill();
            indicatorTransform.DORotate(new Vector3(0, 0, -22f), 0.05f)
                .OnComplete((() => indicatorTransform.DORotate(Vector3.zero, 0.05f)));
            lastCheckedAngle = currentAngle;
        }
    }
}
