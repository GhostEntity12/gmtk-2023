using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnButton : MonoBehaviour
{
	[field: SerializeField] public GameManager.UnitTypes UnitType { get; private set; }
	[SerializeField] int purchaseCost = 10;
	[SerializeField] Button button;
	[SerializeField] TextMeshProUGUI counter;
	[SerializeField] TextMeshProUGUI cost;
	int multiplier = 1;
	public int ActualCost => purchaseCost * multiplier;

	GameManager.GameState gameState;

	[SerializeField] string header;
	[SerializeField, TextArea(3, 5)] string info;
	private void Awake()
	{
		GameManager.OnGameStateChanged += OnGameStateChanged;
	}

	private void OnDestroy()
	{
		GameManager.OnGameStateChanged -= OnGameStateChanged;
	}

	private void Update()
	{
		switch (gameState)
		{
			case GameManager.GameState.Preparation:
				if (Input.GetKey(KeyCode.LeftControl))
				{
					multiplier = 5;
				}
				else
				{
					multiplier = 1;
				}
				UpdatePurchaseCost();
				break;
			case GameManager.GameState.Wave:
				break;
			default:
				break;
		}

	}

	public void OnClick()
	{
		switch (GameManager.Instance.CurrentGameState)
		{
			case GameManager.GameState.Preparation:
				if (ActualCost > GameManager.Instance.Funds.AvailableCurrency) return;
				GameManager.Instance.ClickedPurchase(this);
				break;
			case GameManager.GameState.Wave:
				GameManager.Instance.ClickedSpawn(this);
				GameManager.Instance.TryGetUnit(UnitType, out _, out int count);
				button.interactable = count > 0;
				break;
		}
	}

	public void UpdateCounter(int value)
	{
		counter.SetText($"x{value}");
	}
	 
	public void UpdatePurchaseCost()
	{
		cost.SetText($"<sprite=\"Button\" index=0>{ActualCost}");
		bool canPurchase = ActualCost <= GameManager.Instance.Funds.AvailableCurrency;
		cost.color = canPurchase ? Color.white: Color.red;
		button.interactable = canPurchase;
	}

	void OnGameStateChanged(GameManager.GameState state)
	{
		gameState = state;
		switch (gameState)
		{
			case GameManager.GameState.Preparation:
				SetCostVisibility(true);
				break;
			case GameManager.GameState.Wave:
				SetCostVisibility(false);
				GameManager.Instance.TryGetUnit(UnitType, out _, out int count);
				button.interactable = count > 0; 
				break;
			default:
				break;
		}
	}

	public void SetCostVisibility(bool visible)
	{
		cost.enabled = visible;
	}

	public void DisplayInfo()
	{
		GameManager.Instance.Preview.DisplayInfo(header, info);
	}
	public void ResetInfo()
	{
		GameManager.Instance.Preview.ResetInfo();
	}
}
