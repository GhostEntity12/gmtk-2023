using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Funds
{
    public int AvailableCurrency { get; private set; } = 95;
    [SerializeField] TextMeshProUGUI display;
    
    public void AddFunds(int amount)
    {
        AvailableCurrency += amount;
        UpdateText();
    }

    public void RemoveFunds(int amount)
    {
        AvailableCurrency -= amount;
        UpdateText();
    }

    public void UpdateText()
    {
        display.SetText($"${AvailableCurrency}");
    }
}
