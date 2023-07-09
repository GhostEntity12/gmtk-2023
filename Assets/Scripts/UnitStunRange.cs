using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStunRange : Unit
{
	[SerializeField] float stunDuration;
	[SerializeField] float stunRange;
	protected override void Die()
	{
		List<AICatTower> towersInRange = GameManager.Instance.Towers.Where(t => t.Cat).Where(t => Vector3.Distance(transform.position, t.catLocation.position) < stunRange).ToList();
		foreach (AICatTower tower in towersInRange)
		{
			tower.Cat.Stun(stunDuration);
		}
		base.Die();
	}
}
