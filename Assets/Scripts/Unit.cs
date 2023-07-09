using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Unit : MonoBehaviour
{
	enum MotionType { None, Rotating, Facing }
	[SerializeField] MotionType motionType = MotionType.None;
	[field: SerializeField] public GameManager.UnitTypes UnitType { get; private set; }
	[SerializeField] float speed = 1f;
	[SerializeField] int health = 10;
	public int CurrentPathIndex { get; private set; } = 0;
	Vector3 currentTarget;
	readonly float minDist = 0.01f;
	[field: SerializeField] public int Attractiveness { get; private set; } = 1;
	Vector2 rotSpeedBounds = new(140f, 200f);
	float rotSpeed = 2;
	[SerializeField] int value = 1;

	private void Start()
	{
		if (motionType == MotionType.Rotating)
		{
			rotSpeed = Random.Range(rotSpeedBounds.x, rotSpeedBounds.y);
		}
		else if (motionType == MotionType.Facing)
		{
			transform.LookAt(GameManager.Instance.Path.GetWayPointByIndex(CurrentPathIndex + 1));
		}
		currentTarget = GameManager.Instance.Path.GetWayPointByIndex(CurrentPathIndex);
	}

	// Update is called once per frame
	void Update()
	{
		switch (motionType)
		{
			case MotionType.Rotating:
				transform.RotateAround(transform.position, Vector3.up, rotSpeed * Time.deltaTime);
				break;
			case MotionType.Facing:
				transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, currentTarget - transform.position, rotSpeed * Time.deltaTime, 0));
				break;
			default:
				break;
		}

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

	protected virtual void Die()
	{
		GameManager.Instance.RemoveUnit(this);
		Destroy(gameObject);
		GameManager.Instance.AI.Funds.AddFunds(value);
	}
}
