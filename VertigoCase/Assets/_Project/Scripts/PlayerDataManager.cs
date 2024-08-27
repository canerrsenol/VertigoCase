using KBCore.Refs;
using UnityEngine;

public class PlayerDataManager : ValidatedMonoBehaviour
{
    [SerializeField, Anywhere] private IntEventChannelSO onSpinMoney;
    [SerializeField, Anywhere] private IntEventChannelSO onSpinGold;
    
    [SerializeField, Anywhere] private IntEventChannelSO onMoneyChanged;
    [SerializeField, Anywhere] private IntEventChannelSO onGoldChanged;
    
    private PlayerData playerData;
    
    private void Start()
    {
        playerData = new PlayerData
        {
            Money = 0,
            Gold = 0
        };
    }
    
    private void OnEnable()
    {
        onSpinMoney.OnEventRaised += AddOnSpinMoney;
        onSpinGold.OnEventRaised += AddOnSpinGold;
    }
    
    private void OnDisable()
    {
        onSpinMoney.OnEventRaised -= AddOnSpinMoney;
        onSpinGold.OnEventRaised -= AddOnSpinGold;
    }
    
    private void AddOnSpinMoney(int value)
    {
        playerData.Money += value;
        onMoneyChanged.RaiseEvent(playerData.Money);
    }
    
    private void AddOnSpinGold(int value)
    {
        playerData.Gold += value;
        onGoldChanged.RaiseEvent(playerData.Gold);
    }
}

public struct PlayerData
{
    public int Money;
    public int Gold;
}
