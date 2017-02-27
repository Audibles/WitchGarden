using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : MonoBehaviour {

	public PlayerMovement player;
	GameObject so;
	Destructible sensedObj;
	Vector3 initPos;
	float colliderOffset = 0.255f;

	// Use this for initialization
	void Start () {
		initPos = new Vector3 (0.255f, 0, 0);
		transform.localPosition = initPos;
		so = null;
	}

	public void flipUp() {
		Vector3 newPos = new Vector3 (0, colliderOffset, 0);
		transform.localPosition = newPos;
	}

	public void flipDown() {
		Vector3 newPos = new Vector3 (0, -colliderOffset, 0);
		transform.localPosition = newPos;
	}

	public void flipLeft() {
		Vector3 newPos = new Vector3 (-colliderOffset, 0, 0);
		transform.localPosition = newPos;
	}

	public void flipRight() {
		Vector3 newPos = new Vector3 (colliderOffset, 0, 0);
		transform.localPosition = newPos;
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
