using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour

{
    [HideInInspector] public GameObject projectilePrefab;
    public Player player;
    [HideInInspector] public float projectileForce = 500f;

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, player.transform.position, player.transform.rotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            if (player.transform.localScale.x == 1) projectileRb.velocity = player.transform.right * projectileForce;
            else projectileRb.velocity = -player.transform.right * projectileForce;
        }
        else {
            projectileRb.velocity = new Vector3(
            (Input.GetAxis("Horizontal") < 0) ? Mathf.Floor(Input.GetAxis("Horizontal")) * projectileForce : Mathf.Ceil(Input.GetAxis("Horizontal")) * projectileForce,
            (Input.GetAxis("Vertical") < 0) ? Mathf.Floor(Input.GetAxis("Vertical")) * projectileForce : Mathf.Ceil(Input.GetAxis("Vertical")) * projectileForce,
            0);
        }
    }
}
