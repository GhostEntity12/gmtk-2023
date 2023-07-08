using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatBlack : Cat
{
	[SerializeField] float attackAngle = 22.5f; // note this is effectively doubled because it's the angle in both directions
	[SerializeField] float attackRange = 2f; // note this is effectively doubled because it's the angle in both directions

	protected override void Attack(Unit unit)
	{
		Debug.Log("attacking");
		// get the units in the range
		List<Unit> targetsInRange = unitsInRange.Where(u => Vector3.Angle(transform.forward, u.unit.transform.position - transform.position) < attackAngle &&
															Vector3.Distance(transform.position, u.unit.transform.position) < attackRange)
												.Select(u => u.unit)
												.ToList();
		
		// damage them
		foreach (Unit target in targetsInRange)
		{
			target.TakeDamage(damage);
		}
	}
}
