using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[field: SerializeField] public GameManager.UnitTypes UnitType { get; private set; }
	[SerializeField] float speed = 1f;
	[SerializeField] int health = 10;
	public int CurrentPathIndex { get; private set; }
	Vector3 currentTarget;
	readonly float minDist = 0.01f;
	[field: SerializeField] public int Attractiveness { get; private set; } = 1;

	private void Start()
	{
		currentTarget = GameManager.Instance.Path.GetWayPointByIndex(CurrentPathIndex);
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(currentTarget, transform.position) < minDist)
		{
			if (CurrentPathIndex >= GameManager.Instance.Path.PathLength) return;

			CurrentPathIndex++;
			currentTarget = GameManager.Instance.Path.GetWayPointByIndex(CurrentPathIndex);
		}
		transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		GameManager.Instance.RemoveUnit(this);
		Destroy(gameObject);
	}
}
