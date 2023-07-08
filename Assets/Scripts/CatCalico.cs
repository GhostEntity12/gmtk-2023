using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCalico : Cat
{
	protected override void Attack(Unit unit)
	{
		unit.TakeDamage(damage);
	}
}
