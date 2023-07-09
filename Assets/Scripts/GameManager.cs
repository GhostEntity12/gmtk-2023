using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	// Events
	public delegate void GameStateChanged(GameState state);
	public static event GameStateChanged OnGameStateChanged;

	// Enums
	public enum UnitTypes { Feather, Fish, Mouse, PlasticBall, StringBall, Roomba }
	public enum GameState { Preparation, Wave }

	// Storage
	readonly List<GroupSpawn> groupSpawns = new(); // Units part of a group spawn
	readonly Dictionary<UnitTypes, int> purchasedUnits = new(); // Units that have been purchased but not sent
	readonly List<Unit> spawnedUnits = new(); // Currently active units
	int UnitsInStorage => purchasedUnits.Sum(u => u.Value);

	public GameState CurrentGameState = GameState.Preparation;

	[SerializeField] List<Unit> units = new();

	public UnitPath Path { get; private set; }

	[field: SerializeField] public Funds Funds { get; private set; } = new Funds();

	public AICatPlacer AI { get; private set; }

	[field: SerializeField] public List<AICatTower> Towers { get; private set; } = new();


	// Start is called before the first frame update
	void Start()
	{
		// Update UI
		Funds.UpdateText();

		// Get objects
		Path = GetComponent<UnitPath>();
		AI = GetComponent<AICatPlacer>();

		// Initialize dictionary
		foreach (UnitTypes unitType in Enum.GetValues(typeof(UnitTypes)))
		{
			purchasedUnits.Add(unitType, 0);
		}
		AI.Funds.AddFunds(10);	
		Funds.AddFunds(100);

		AI.DoPlacing();
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

	/// <summary>
	/// Runs every frame while in the preparation state
	/// </summary>	
	void UpdatePreparation()
	{
		return;
	}

	/// <summary>
	/// Runs every frame while in the wave state
	/// </summary>
	void UpdateWave()
	{
		if (UnitsInStorage == 0 && spawnedUnits.Count == 0 && groupSpawns.Count == 0 )
		{
			// There are no more units remaining
			ToggleGameState();
		}

		// Skip if no group spawns
		if (groupSpawns.Count <= 0) return;

		// Tick all group spawns and remove those that are empty
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
		Unit u = Instantiate(unit, Path.GetWayPointByIndex(0), Quaternion.identity);
		//u.gameObject.name = Time.time.ToString();
		spawnedUnits.Add(u);
	}

	private void SpawnUnitMultiple(Unit unit, int count)
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

	public void ToggleGameState()
	{
		CurrentGameState = (GameState)((int)(CurrentGameState + 1) % 2);
		OnGameStateChanged?.Invoke(CurrentGameState);
		if (CurrentGameState== GameState.Preparation)
		{
			AI.Funds.AddFunds(5);
			AI.DoPlacing();
		}
	}

	public void RemoveUnit(Unit u)
	{
		spawnedUnits.Remove(u);
	}
}