using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SphereCollider))]
public abstract class Cat : MonoBehaviour
{
	[Tooltip("How much it costs to purchase the cat")]
	[field: SerializeField] public int Cost { get; private set; } = 10;

	// Attacks
	float cooldownTimer = 0;
	[Tooltip("Time between attacks")]
	[SerializeField] protected float attackCooldown = 0.5f;
	[Tooltip("Damage to do when attacking")]
	[SerializeField] protected int damage = 5;

	// Stun
	bool isStunned = false;
	float stunTimer;
	[SerializeField] ParticleSystem stunShort, stunLong;

	// Targeting
	SphereCollider visionRange;
	protected List<(Unit unit, float timestamp)> unitsInRange = new();
	protected Unit TargetUnit => unitsInRange.Count > 0 ? unitsInRange[0].unit : null;
	protected float Radius => visionRange.radius;

	[field: SerializeField] public string Header { get; private set; }
	[field: SerializeField, TextArea(3, 5)] public string Info { get; private set; } 

	private void Awake()
	{
		visionRange = GetComponent<SphereCollider>();
	}

	// Update is called once per frame
	void Update()
	{
		if (isStunned)
		{
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0)
			{
				isStunned = false;
			}
			return;
		}
		// Removed all destroyed (null) units from the list
		unitsInRange.RemoveAll(u => u.unit == null);
		if (TargetUnit)
		{
			// Look at the target and reset the x/z rotation
			transform.LookAt(TargetUnit.transform.position);
			transform.eulerAngles = new(0, transform.rotation.eulerAngles.y, 0);
			
			if (cooldownTimer == 0)
			{
				Attack(TargetUnit);
				cooldownTimer = attackCooldown;
			}
		}

		if (cooldownTimer > 0)
		{
			cooldownTimer = Mathf.Max(0, cooldownTimer - Time.deltaTime);
		}
	}

	protected abstract void Attack(Unit unit);

	public void Stun(float stunDuration)
	{
		isStunned = true;
		stunTimer = stunDuration;
		if (stunDuration < 3)
		{
			stunShort.Play();
		}
		else
		{
			stunLong.Play();
		}
	}

	// Targeting
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Unit u))
		{
			unitsInRange.Add((u, Time.time));
			unitsInRange = unitsInRange.OrderByDescending(u => u.unit.Attractiveness).ThenByDescending(u => u.unit.CurrentPathIndex).ThenBy(u => u.timestamp).ToList();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out Unit u))
		{
			unitsInRange.RemoveAll(t => t.unit == u);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 1, 1, 0.5f);
		Gizmos.DrawSphere(transform.position, GetComponent<SphereCollider>().radius);
	}
}
