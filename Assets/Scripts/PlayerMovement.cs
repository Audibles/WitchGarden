using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	float moveX;
	float moveY;
	public float speed;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Later, we will probably have to change the movement to be 8-directional (or 4-directional?)
		// and to make the sprite turn/animate when it changes directions.
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");
		Vector2 delta = new Vector2(moveX * speed, moveY * speed);
		if (delta.magnitude > speed) {
			delta.Normalize ();
			delta = delta * speed;
		}
		rb.velocity = delta;
	}
}
