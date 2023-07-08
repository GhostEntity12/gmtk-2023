using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[field: SerializeField] public GameManager.UnitTypes UnitType { get; private set; }
	[SerializeField] float speed = 1f;
	[SerializeField] int health = 10;
	int currentPathIndex;
	Vector3 currentTarget;
	readonly float minDist = 0.01f;

	private void Start()
	{
		currentTarget = GameManager.Instance.Path.GetWayPointByIndex(currentPathIndex);
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(currentTarget, transform.position) < minDist)
		{
			if (currentPathIndex >= GameManager.Instance.Path.PathLength) return;

			currentPathIndex++;
			currentTarget = GameManager.Instance.Path.GetWayPointByIndex(currentPathIndex);
		}
		transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		if (health <= 0)
			Die();
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
