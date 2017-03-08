using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : Sensor {

	public PlayerMovement player;
	GameObject so;
	Destructible sensedObj;
	Vector3 initPos;

	// Use this for initialization
	void Start () {
		base.Start ();
		colliderOffset = 0.255f;
		initPos = new Vector3 (colliderOffset, 0, 0);
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Destructible") {
			so = GameObject.Find(other.gameObject.name);
			sensedObj = (Destructible) so.GetComponent(typeof(Destructible));
			player.sensingDestructible = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		so = null;
		sensedObj = null;
		player.sensingDestructible = false;
	}

	public void Damage(PlayerMovement player) {
		sensedObj.TakeDamage (player);
	}
}
