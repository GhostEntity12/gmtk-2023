using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStunClosest : Unit
{
	[SerializeField] float stunDuration;
	protected override void Die()
	{
		AICatTower closestFilledCatTower = GameManager.Instance.Towers.Where(t => t.Cat).OrderBy(t => Vector3.Distance(transform.position, t.catLocation.position)).FirstOrDefault();
		if (closestFilledCatTower != null)
		{
			closestFilledCatTower.Cat.Stun(stunDuration);
		}
		base.Die();
	}
}
