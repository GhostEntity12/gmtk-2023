using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	int damage = 1;
	float speed = 1;

	public void Setup(int damage, float speed, Quaternion rotation)
	{
		this.damage = damage;
		this.speed = speed;
		transform.rotation = rotation;
	}

	private void Update()
	{
		transform.Translate(speed * Time.deltaTime * transform.forward);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Unit unit))
		{
			unit.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}
