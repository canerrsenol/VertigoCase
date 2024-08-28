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
    [SerializeField, Anywhere] private VoidEventChannelSO onLeavingGame;
    [SerializeField, Anywhere] private VoidEventChannelSO onExitButtonClicked;
    
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
        onExitButtonClicked.OnEventRaised += TryingToExitGame;
    }

    private void OnDisable()
    {
        onSpinWheelClicked.OnEventRaised -= Spin;
        onSpinReset.OnEventRaised -= ResetSpin;
        onExitButtonClicked.OnEventRaised -= TryingToExitGame;
    }

    private void TryingToExitGame()
    {
        if(isSpinning) { return; }
        if(!currentZoneSettings.isRiskFree) { return; }
        ResetSpin();
        onLeavingGame.RaiseEvent();
    }

    private void ResetSpin()
    {
        zoneCounter = 1;
        onZoneCounterChanged.RaiseEvent(zoneCounter);
        currentZoneSettings = bronzeZoneSettings;
        spinWheelVisual.SetSpinWheelVisual(bronzeZoneSettings);
        UpdateSliceItems();
    }

    private void Spin()
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
        UpdateSliceItems();
    }

    private void UpdateSliceItems()
    {
        if (currentZoneSettings.isRiskFree)
        {
            for (int i = 0; i < spinSlices.Length; i++)
            {
                SetRandomItemToSlice(i);
            }
        }
        else
        {
            int bombIndex = Random.Range(0, spinSlices.Length);
            spinSlices[bombIndex].SetItem(bombItem, 1);
            
            for (int i = 0; i < spinSlices.Length; i++)
            {
                if (i == bombIndex) { continue; }
                SetRandomItemToSlice(i);
            }
        }
    }

    private void SetRandomItemToSlice(int i)
    {
        var randomItem = currentZoneSettings.canHaveItems[Random.Range(0, currentZoneSettings.canHaveItems.Length)];
        int multiplier =  zoneCounter * Random.Range(currentZoneSettings.zoneMultiplier.x , currentZoneSettings.zoneMultiplier.y);
        spinSlices[i].SetItem(randomItem, multiplier);
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
