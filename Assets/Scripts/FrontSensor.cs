using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : MonoBehaviour {

	Vector3 initPos;
	float colliderOffset = 0.255f;

	// Use this for initialization
	void Start () {
		initPos = new Vector3 (0.255f, 0, 0);
		transform.localPosition = initPos;
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
}
