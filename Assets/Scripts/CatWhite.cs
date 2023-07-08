using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWhite : Cat
{
	[SerializeField] ProjectilePiercing projectilePrefab;
	[SerializeField] float projectileSpeed = 1f;
	[SerializeField] int pierceDepth;
	[SerializeField] int attacksToMake = 1;
	[SerializeField] Vector3 spawnPointOffset;
	[SerializeField] float spacing;

	protected override void Attack(Unit unit)
	{
		for (int i = 0; i < attacksToMake; i++)
		{
			ProjectilePiercing p = Instantiate(projectilePrefab);
			p.Setup(damage, projectileSpeed, transform.rotation, pierceDepth);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new(0, 0.6f, 0, 0.5f);

		for (int i = 0; i < attacksToMake; i++)
		{
			Gizmos.DrawSphere(transform.TransformPoint(spawnPointOffset) + Vector3.right * ((-(spacing / 2) * (attacksToMake - 1)) + (i * spacing)), 0.12f);
		}
	}
}
