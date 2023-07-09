using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOE : Projectile
{
	int aoeDamage;
	float aoeRadius;

	public void Setup(int damage, float speed, Quaternion rotation, int aoeDamage, float aoeRadius)
	{
		base.Setup(damage, speed, rotation);
		this.aoeDamage = aoeDamage;
		this.aoeRadius = aoeRadius;
	}

	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent(out Unit unit)) return;

		// Base damage
		unit.TakeDamage(damage);
		
		// AoE damage
		Collider[] units = Physics.OverlapSphere(transform.position, aoeRadius, LayerMask.NameToLayer("Unit"));
		foreach (Collider c in units)
		{
			// All colliders with a unit component which aren't the unit that was already hit.
			if (c.TryGetComponent(out Unit u) && u != unit)
			{
				u.TakeDamage(aoeDamage);
			}
		}
		Destroy(gameObject);
	}
}
