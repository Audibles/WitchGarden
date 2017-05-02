using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBush : Destructible
{
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    public AudioClip breakSound1;
    public AudioClip breakSound2;
    public AudioClip breakSound3;
    public AudioClip breakSound4;
    public AudioClip breakSound5;
    private AudioSource source;
    private bool at_crit_health;


    Animator anim;
    private SpriteRenderer spriteRenderer;
    public Sprite normal; 
    public Sprite health8;
    public Sprite health6;
    public Sprite health4;
    public Sprite health2;
    public Sprite health0;

    private int hitscoreValue;

    // Use this for initialization
    public override void Start () {
        base.Start();
        scoreValue = 25;
        hitscoreValue = 5;
        health = 9;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = normal;
        }
        source = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
        at_crit_health = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ChosenAudio() {
        var number = Random.Range(1, 6);
        float vol = Random.Range(volLowRange, volHighRange);

        if (number == 1) {
            source.PlayOneShot( breakSound1, vol);
        } else if (number == 2)
        {
            source.PlayOneShot(breakSound2, vol);
        }
        else if (number == 3)
        {
            source.PlayOneShot(breakSound3, vol);
        }
        else if (number == 4)
        {
            source.PlayOneShot(breakSound4, vol);
        }
        else if (number == 5)
        {
            source.PlayOneShot(breakSound5, vol);
        }

    }

    public override void TakeDamage(PlayerMovement player)
    {
        ChosenAudio();
        player.score += hitscoreValue;
        health -= 1;
        if (health == 8) {
            print("AT 8 HEALTH");
            spriteRenderer.sprite = health8;
        } else if (health == 6) {
            print("AT 6 HEALTH");
            spriteRenderer.sprite = health6;
            //change to the next state
        } else if (health == 4) {
            print("AT 4 HEALTH");
            spriteRenderer.sprite = health4;
            at_crit_health = true;
            //change to the next state
        } else if (health == 2) {
            print("AT 2 HEALTH");
            spriteRenderer.sprite = health2;
            //change to the next state
        } else if (health == 0) {
            print("AT 0 HEALTH");
            spriteRenderer.sprite = health0;
            //change to the next state
            GetDestroyed(player);
        }
    }

    public bool AtCritHealth() {
        return at_crit_health;
    }

    public override void GetDestroyed(PlayerMovement player)
    {
        //anim.SetBool("Hit", true);
        player.score += scoreValue;
        //print("DESTROYED");
        Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());

    }

    public void DamageOverTime(PlayerMovement player, float time) {
        float curr_time = Time.time;
        while (curr_time - time != Time.time) {
            TakeDamage(player);
        }
    }
}
