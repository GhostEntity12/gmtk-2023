using System;
using TMPro;
using UnityEngine;

public class UnitSpawnButton : MonoBehaviour
{
	[field: SerializeField] public GameManager.UnitTypes UnitType { get; private set; }
	[SerializeField] int purchaseCost = 10;
	[SerializeField] TextMeshProUGUI counter;
	[SerializeField] TextMeshProUGUI cost;
	int multiplier = 1;
	public int ActualCost => purchaseCost * multiplier;

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
		if (Input.GetKey(KeyCode.LeftControl))
		{
			multiplier = 5;
		}
		else
		{
			multiplier = 1;
		}

		UpdatePurchaseCost();
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
				break;
		}
	}

	public void UpdateCounter(int value)
	{
		counter.SetText(value.ToString());
	}
	 
	public void UpdatePurchaseCost()
	{
		cost.SetText($"${ActualCost}");
		cost.color = ActualCost > GameManager.Instance.Funds.AvailableCurrency ? Color.red : Color.white;
	}

	void OnGameStateChanged(GameManager.GameState state)
	{
		Debug.Log(state.ToString());
		switch (state)
		{
			case GameManager.GameState.Preparation:
				SetCostVisibility(true);
				break;
			case GameManager.GameState.Wave:
				SetCostVisibility(false);
				break;
			default:
				break;
		}
	}

	public void SetCostVisibility(bool visible)
	{
		cost.enabled = visible;
	}
}
