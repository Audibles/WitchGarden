using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour {

	GameObject so; //sensed object
	Vector3 initPos;
	public float colliderOffset;

	// Use this for initialization
	public virtual void Start () {
		transform.localPosition = initPos;
		so = null;
	}

	public virtual void flipUp() {
		Vector3 newPos = new Vector3 (0, colliderOffset, 0);
		transform.localPosition = newPos;
	}

	public virtual void flipDown() {
		Vector3 newPos = new Vector3 (0, -colliderOffset, 0);
		transform.localPosition = newPos;
	}

	public virtual void flipLeft() {
		Vector3 newPos = new Vector3 (-colliderOffset, 0, 0);
		transform.localPosition = newPos;
	}

	public virtual void flipRight() {
		Vector3 newPos = new Vector3 (colliderOffset, 0, 0);
		transform.localPosition = newPos;
	}
}
