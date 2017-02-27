using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Destructible {

	// Use this for initialization
	void Start () {
		base.Start ();
		scoreValue = 10;
		health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (health);
	}

	public override void TakeDamage(PlayerMovement player) {
		health -= 1;
		if (health == 0) {
			GetDestroyed (player);
		}
	}

	public override void GetDestroyed(PlayerMovement player) {
		//anim.SetBool("Hit", true);
		player.score += scoreValue;
		destroyed = true;
		Destroy (this.gameObject);
	}
}


