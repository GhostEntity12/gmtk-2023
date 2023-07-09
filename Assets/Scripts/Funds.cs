using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Funds
{
    public int AvailableCurrency { get; private set; }
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
        if (display)
            display.SetText($"<sprite=\"Button\" index=0>{AvailableCurrency}");
    }
}
