using UnityEngine;

public class GroupSpawn
{
	readonly Unit unit;
	readonly float interval = 0.375f;
	int remainingSpawns;
	float timer;

	public GroupSpawn(Unit unit, int spawns)
	{
		this.unit = unit;
		remainingSpawns = spawns;
		timer = 0;
	}

	public bool Tick()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			timer += interval;
			GameManager.Instance.SpawnUnit(unit);
			remainingSpawns--;
		}
		return remainingSpawns == 0;
	}
}
