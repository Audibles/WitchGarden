using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSensor : Sensor {

	public GuardMovement guard;
	GameObject so;
	PlayerMovement sensedObj;
	Vector3 initPos;

	// Use this for initialization
	void Start () {
		base.Start ();
		colliderOffset = 0.52f;
		initPos = new Vector3 (colliderOffset, 0, 0);
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			so = GameObject.Find(other.gameObject.name);
			sensedObj = (PlayerMovement) so.GetComponent(typeof(PlayerMovement));
			sensedObj.getCaught();
		}
	}
}
