using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    private AudioSource levelMusic;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        levelMusic = GetComponent<AudioSource>();
        player = GameManager.instance.player;
        levelMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive)
        {
            levelMusic.Stop();
        }
    }
}
