using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : Collidable
{
    public string[] sceneNames;
    public Image fade;
    public Animator anim;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // Move the player to the new scene on collision
            AudioManager.PlayClipStatic("Ladder");
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        // Scene change fade
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        // Load next scene and save game state
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.instance.SaveState();
    }
}
