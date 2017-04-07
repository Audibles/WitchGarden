using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBush : Destructible
{
    //private float volLowRange = .5f;
    //private float volHighRange = 1.0f;
    //public AudioClip breakSound;
    //private AudioSource source;
    Animator anim;
    private SpriteRenderer spriteRenderer;
    public Sprite normal; 
    public Sprite health8;
    public Sprite health6;
    public Sprite health4;
    public Sprite health2;
    public Sprite health0;

    // Use this for initialization
    public override void Start () {
        base.Start();
        scoreValue = 25;
        health = 10;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = normal;
        }
        //source = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TakeDamage(PlayerMovement player)
    {
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

    public override void GetDestroyed(PlayerMovement player)
    {
        //anim.SetBool("Hit", true);
        player.score += scoreValue;
        //print("DESTROYED");
        Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());

    }
}
