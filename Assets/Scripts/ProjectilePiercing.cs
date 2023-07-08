using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePiercing : Projectile
{
	int pierceDepth;

    public void Setup(int damage, float speed, Quaternion rotation, int pierceDepth)
    {
        base.Setup(damage, speed, rotation);
        this.pierceDepth = pierceDepth;
    }
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Unit unit))
		{
			unit.TakeDamage(damage);
			if (--pierceDepth <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
