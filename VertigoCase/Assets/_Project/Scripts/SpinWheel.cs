using UnityEngine;
using DG.Tweening;
using KBCore.Refs;

public class SpinWheel : ValidatedMonoBehaviour
{
    [SerializeField] private float spinDuration;
    [SerializeField] private float baseSpinAngle;
    [SerializeField] private AnimationCurve spinCurve;
    
    private float spinAngleForSlice;
    private int currentSliceIndex;
    private bool isSpinning;

    private int zoneCounter;
    
    [SerializeField, Anywhere] private IntEventChannelSO onZoneCounterChanged;
    [SerializeField, Anywhere] private VoidEventChannelSO onSpinWheelClicked;
    
    [SerializeField, Anywhere] private IntEventChannelSO onSpinMoney;
    [SerializeField, Anywhere] private IntEventChannelSO onSpinGold;
    [SerializeField, Anywhere] private IntEventChannelSO onSpinBomb;
    [SerializeField, Anywhere] private ChestItemChannelSO onSpinChest;
    
    // For indicator animation
    [SerializeField, Anywhere] private Transform indicatorTransform;
    private float lastCheckedAngle;

    [SerializeField, Child] private SpinSlice[] spinSlices;
    
    private void Start()
    {
        spinAngleForSlice = 360f / spinSlices.Length;
        zoneCounter = 1;
        onZoneCounterChanged.RaiseEvent(zoneCounter);
    }
    
    private void OnEnable()
    {
        onSpinWheelClicked.OnEventRaised += Spin;
        
        onSpinMoney.OnEventRaised += OnSpinMoney;
        onSpinGold.OnEventRaised += OnSpinGold;
        onSpinBomb.OnEventRaised += OnSpinBomb;
        onSpinChest.OnEventRaised += OnSpinChest;
    }

    private void OnDisable()
    {
        onSpinWheelClicked.OnEventRaised -= Spin;
        
        onSpinMoney.OnEventRaised -= OnSpinMoney;
        onSpinGold.OnEventRaised -= OnSpinGold;
        onSpinBomb.OnEventRaised -= OnSpinBomb;
        onSpinChest.OnEventRaised -= OnSpinChest;
    }
    
    public void Spin()
    {
        if (isSpinning) { return; }
        
        int randomSlotIndex = Random.Range(0, spinSlices.Length);
        float angleToSpin = randomSlotIndex * spinAngleForSlice;
        
        isSpinning = true;
        transform.DORotate(new Vector3(0, 0, baseSpinAngle + angleToSpin), spinDuration, RotateMode.WorldAxisAdd)
            .SetEase(spinCurve)
            .OnUpdate(CheckIndicatorAnimation)
            .OnComplete(() =>
            {
                currentSliceIndex = randomSlotIndex;
                spinSlices[currentSliceIndex].SpinSliceAction();
                PrepareForNextSpin();
                Debug.Log("Spin completed at index: " + currentSliceIndex);
            });
    }
    
    private void OnSpinChest(ChestType arg1, int arg2)
    {
        
    }

    private void OnSpinBomb(int multiplier)
    {
        // Game over
    }

    private void OnSpinGold(int obj)
    {
        isSpinning = false;
    }

    private void OnSpinMoney(int obj)
    {
        isSpinning = false;
    }

    private void PrepareForNextSpin()
    {
        // Update zone counter
        zoneCounter++;
        onZoneCounterChanged.RaiseEvent(zoneCounter);
        
        // Update slice multipliers and items
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
