using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FireAbility")]
public class FireAbility : Ability
{
	public float projectileForce = 1f;
	public GameObject fireProjectilePrefab;

	private ProjectileBehaviour projectileBehaviour;

	public override void InitializeAbility(GameObject obj)
	{
		projectileBehaviour = obj.GetComponent<ProjectileBehaviour>();
		projectileBehaviour.projectileForce = projectileForce;
		projectileBehaviour.projectilePrefab = fireProjectilePrefab;
	}

	public override void TriggerAbility()
	{
		projectileBehaviour.Shoot();
	}
}
