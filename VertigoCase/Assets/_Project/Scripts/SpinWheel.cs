using UnityEngine;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine.Serialization;

public class SpinWheel : ValidatedMonoBehaviour
{
    [SerializeField] private float spinDuration;
    [SerializeField] private float baseSpinAngle;
    [SerializeField] private AnimationCurve spinCurve;
    
    private float spinAngleForSlice;
    private int currentSlotIndex;
    private bool isSpinning;
    
    // For indicator animation
    [SerializeField, Anywhere] private Transform indicatorTransform;
    private float lastCheckedAngle;

    [SerializeField, Child] private SpinSlice[] spinSlots;
    private void Start()
    {
        spinAngleForSlice = 360f / spinSlots.Length;
    }
    
    public void Spin()
    {
        if (isSpinning)
        {
            return;
        }
        
        int randomSlotIndex = Random.Range(0, spinSlots.Length);
        float angleToSpin = randomSlotIndex * spinAngleForSlice;
        
        isSpinning = true;
        transform.DORotate(new Vector3(0, 0, baseSpinAngle + angleToSpin), spinDuration, RotateMode.FastBeyond360)
            .SetEase(spinCurve)
            .OnUpdate(CheckIndicatorAnimation)
            .OnComplete(() =>
            {
                isSpinning = false;
                currentSlotIndex = randomSlotIndex;
                Debug.Log("Spin Completed at Slot: " + currentSlotIndex);
            });
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
