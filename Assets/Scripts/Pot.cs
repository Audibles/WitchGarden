using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Destructible
{

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    public AudioClip breakSound;
    private AudioSource source;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        base.Start();
        scoreValue = 10;
        health = 1;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        //anim.SetBool("breakPot", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TakeDamage(PlayerMovement player)
    {
        health -= 1;

        if (health == 0)
        {

            anim.SetBool("breakPot", true);
            //anim.SetTrigger("breakPot");
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(breakSound, vol);
            print("triggered");
            StartCoroutine(ExecuteAfterTime(2, player));
        }
    }

    public IEnumerator ExecuteAfterTime(float time, PlayerMovement player)
    {
        yield return new WaitForSeconds(time);
        GetDestroyed(player);
        anim.SetBool("potGone", true);

    }

    public override void GetDestroyed(PlayerMovement player)
    {
        //anim.SetBool("Hit", true);
        player.score += scoreValue;
        destroyed = true;
        Destroy(this.gameObject);
    }
}