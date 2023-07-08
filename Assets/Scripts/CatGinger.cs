using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGinger : Cat
{
	[SerializeField] ProjectileAOE projectilePrefab;
	[SerializeField] float projectileSpeed = 1f;
	[SerializeField] int aoeDamage = 1;
	[SerializeField] float aoeRadius = 1f;
	[SerializeField] Vector3 spawnPointOffset;
	protected override void Attack(Unit unit)
	{
		ProjectileAOE p = Instantiate(projectilePrefab, transform.TransformPoint(spawnPointOffset), Quaternion.identity);
		p.Setup(damage, projectileSpeed, transform.rotation, aoeDamage, aoeRadius);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new(0, 0.6f, 0, 0.5f);
		Gizmos.DrawSphere(transform.TransformPoint(spawnPointOffset), 0.12f);
	}
}
