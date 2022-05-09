using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGiant : Damageable
{
	// Experience given to player
	public int xpValue = 10;

	public GameObject deathEffect;
	public GameObject axe;

	private Transform currentTransform;
	private Transform playerTransform;
	private bool isFlipped = false;

	protected virtual void Start()
    {
		playerTransform = GameManager.instance.player.transform;
	}

	protected virtual void Update()
    {
		currentTransform = transform;

	}

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x < playerTransform.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x > playerTransform.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	protected override void Death()
	{
		Instantiate(deathEffect, currentTransform.position, Quaternion.identity);
		Destroy(gameObject);
		GameManager.instance.GrantXp(xpValue);
		GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.green, transform.position, Vector3.up * 50, 1f);
		Instantiate(axe, GameManager.instance.weapons.transform).SetActive(false);
		GameManager.instance.ShowText(axe.GetComponent<Weapon>().weaponName + " obtained!", 30, Color.yellow, transform.position, Vector3.right * 30, 2f);
	}
}