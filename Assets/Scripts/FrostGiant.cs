using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostGiant : Damageable
{
	// Experience given to player
	public int xpValue = 10;

	// Logic
	public GameObject deathEffect;
	public GameObject winScreen;
	public GameObject levelMusic;
	[HideInInspector]
	public AudioSource audioSource;

	private Transform playerTransform;
	private bool isFlipped = false;

	protected virtual void Start()
    {
		GameManager.instance.bossBeaten = false;
		winScreen.SetActive(false);
		playerTransform = GameManager.instance.player.transform;
		audioSource = GetComponent<AudioSource>();
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

	protected override void ReceiveDamage(Damage dmg)
	{
		base.ReceiveDamage(dmg);
		AudioManager.PlayClipStatic("GiantDamage");
	}

	IEnumerator IceEffect()
	{
		GameManager.instance.ShowText("Ice has no effect on Ice Giant!", 30, Color.cyan, transform.position, Vector3.up * 50, 2f);
		yield return new WaitForSeconds(2.0f);
	}

	IEnumerator FireEffect(Damage dmg)
	{
		GameManager.instance.ShowText("Enemy is burning!", 30, Color.red, transform.position, Vector3.up * 50, 1f);
		ReceiveDamage(dmg);
		yield return new WaitForSeconds(1.0f);
		ReceiveDamage(dmg);
		yield return new WaitForSeconds(1.0f);
		ReceiveDamage(dmg);
		yield return new WaitForSeconds(1.0f);
		ReceiveDamage(dmg);
	}

	protected override void Death()
	{
		levelMusic.GetComponent<AudioSource>().Stop();
		AudioManager.PlayClipStatic("GiantDeath");
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
		AudioManager.PlayClipStatic("GiantBeaten");
		GameManager.instance.GrantXp(xpValue);
		GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.green, transform.position, Vector3.up * 50, 1f);
		GameManager.instance.bossBeaten = true;
		GameManager.instance.SaveState();
		GameManager.instance.ResetState();
		GameManager.instance.player.GiveHP(maxHP);
		winScreen.SetActive(true);
		GameManager.instance.ShowText("Giant's axe and ice powers obtained!", 30, Color.yellow, transform.position, Vector3.right * 30, 2f);
	}
}