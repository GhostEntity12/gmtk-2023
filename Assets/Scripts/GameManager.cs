using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	[Serializable]
	public enum UnitTypes { Basic }
	public enum GameState { Preparation, Wave }
	public GameState CurrentGameState = GameState.Preparation;
	public UnitPath Path { get; private set; }
	[SerializeField] List<Unit> units = new();

	readonly List<GroupSpawn> groupSpawns = new();

	readonly Dictionary<UnitTypes, int> purchasedUnits = new();
	[field: SerializeField] public Funds Funds { get; private set; } = new Funds();

	// Start is called before the first frame update
	void Start()
	{
		Funds.UpdateText();
		Path = GetComponent<UnitPath>();

		foreach (UnitTypes unitType in Enum.GetValues(typeof(UnitTypes)))
		{
			purchasedUnits.Add(unitType, 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		switch (CurrentGameState)
		{
			case GameState.Preparation:
				UpdatePreparation();
				break;
			case GameState.Wave:
				UpdateWave();
				break;
			default:
				break;
		}
	}

	void SetGameState(GameState gameState)
	{
		switch (gameState)
		{
			case GameState.Preparation:
				break;
			case GameState.Wave:
				break;
			default:
				break;
		}
	}

	void UpdatePreparation()
	{
		return;
	}

	void UpdateWave()
	{
		if (groupSpawns.Count <= 0) return;

		groupSpawns.RemoveAll(spawn => spawn.Tick());
	}

	public void ClickedSpawn(UnitSpawnButton button)
	{
		if (TryGetUnit(button.UnitType, out Unit unit, out int remaining))
		{
			if (remaining == 0) return;

			if (Input.GetKey(KeyCode.LeftControl))
			{
				int unitsToSpawn = Mathf.Min(5, remaining);
				purchasedUnits[button.UnitType] -= unitsToSpawn;
				SpawnUnitMultiple(unit, unitsToSpawn);
			}
			else
			{
				purchasedUnits[button.UnitType]--;
				SpawnUnit(unit);
			}
			button.UpdateCounter(purchasedUnits[button.UnitType]);
		}
	}

	public void ClickedPurchase(UnitSpawnButton button)
	{
		if (TryGetUnit(button.UnitType, out Unit _, out int _))
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{
				purchasedUnits[button.UnitType] += 5;
			}
			else
			{
				purchasedUnits[button.UnitType]++;
			}
			Funds.RemoveFunds(button.ActualCost);
			button.UpdateCounter(purchasedUnits[button.UnitType]);
			button.UpdatePurchaseCost();
		}
	}

	public void SpawnUnit(Unit unit)
	{
		Instantiate(unit, Path.GetWayPointByIndex(0), Quaternion.identity);
	}

	void SpawnUnitMultiple(Unit unit, int count)
	{
		groupSpawns.Add(new(unit, count));
	}

	public bool TryGetUnit(UnitTypes unitType, out Unit unit, out int remaining)
	{
		unit = null;
		remaining = 0;
		foreach (Unit u in units)
		{
			if (u.UnitType == unitType)
			{
				unit = u;
				remaining = purchasedUnits[unitType];
				return true;
			}
		}
		Debug.LogError($"Unit Type {unitType} is missing from the list of possible units");
		return false;
	}
}