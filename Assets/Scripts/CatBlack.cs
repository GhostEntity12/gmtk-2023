using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatBlack : Cat
{
	[SerializeField] float attackAngle = 22.5f; // note this is effectively doubled because it's the angle in both directions
	[SerializeField] float attackRange = 2f;
	[SerializeField] ParticleSystem particles;
	Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	void UpdateParticleSystem()
	{
		ParticleSystem.ShapeModule s = particles.shape;
		ParticleSystem.MainModule main = particles.main;
		s.arc = attackAngle * 2;
		main.duration = main.startLifetimeMultiplier = attackRange / 4;
	}

	protected override void Attack(Unit unit)
	{
		anim.SetTrigger("attack");
		particles.Play();
		// get the units in the range
		List<Unit> targetsInRange = unitsInRange.Where(u => Vector3.Angle(transform.forward, u.unit.transform.position - transform.position) < attackAngle)
												.Select(u => u.unit)
												.ToList();

		Debug.Log(targetsInRange.Count);

		// damage them
		foreach (Unit target in targetsInRange)
		{
			target.TakeDamage(damage);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(transform.position, transform.forward);
	}
}
