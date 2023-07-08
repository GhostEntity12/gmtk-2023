using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	protected int damage;
	protected float speed;

	public virtual void Setup(int damage, float speed, Quaternion rotation)
	{
		this.damage = damage;
		this.speed = speed;
		transform.rotation = rotation;
	}

	private void Update()
	{
		transform.Translate(speed * Time.deltaTime * Vector3.forward);
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
