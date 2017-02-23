using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	float moveX;
	float moveY;
	public float speed;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// NOTE: atm, this just flips the sprite when the player changes directions.
		// Later, we will replace this with different animations for each direction.
		Vector2 moveDirection = new Vector2(0, 0);
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		if (moveX > 0) { //flip right
			sr.flipX = false;
			moveDirection.x = 1;
		} else if (moveX < 0) { //flip left
			sr.flipX = true;
			moveDirection.x = -1;
		}
		if (moveY > 0) { //flip up
			sr.flipY = false;
			moveDirection.y = 1;
		} else if (moveY < 0) { //flip down
			sr.flipY = true;
			moveDirection.y = -1;
		}

		transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
	}
}
