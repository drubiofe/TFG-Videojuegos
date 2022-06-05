using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/IceAbility")]
public class IceAbility : Ability
{
    public float projectileForce = 1f;
    public GameObject iceProjectilePrefab;

	private ProjectileBehaviour projectileBehaviour;

	public override void InitializeAbility(GameObject obj)
	{
		projectileBehaviour = obj.GetComponent<ProjectileBehaviour>();
		projectileBehaviour.projectileForce = projectileForce;
		projectileBehaviour.projectilePrefab = iceProjectilePrefab;
	}

	public override void TriggerAbility()
	{
		AudioManager.PlayClipStatic("IceCast");
		projectileBehaviour.Shoot();
	}
}
