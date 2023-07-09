using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCalico : Cat
{
	Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}
	protected override void Attack(Unit unit)
	{
		anim.SetTrigger("attack");
		audioSource.PlayOneShot(attackSound, 0.5f);
		unit.TakeDamage(damage);
	}
}
