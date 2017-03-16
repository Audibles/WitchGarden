
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject gs;
	GuardSensor guardSensor;
	public float speed;
	float initPosX;
	float initPosY;
	float[] xPath; //guard will patrol from xPath[0] to xPath[1] on x-axis
	float[] yPath;
	public string facing;
	Vector2 moveDirection;
	Vector3 initPos;
	BoxCollider2D bc;
	float colliderOffset = -0.207f;

	// Use this for initialization
	void Start () {
		gs = GameObject.Find("GuardSensor");
		guardSensor = (GuardSensor) gs.GetComponent(typeof(GuardSensor));

		sr = gameObject.GetComponent<SpriteRenderer> ();
		bc = gameObject.GetComponent<BoxCollider2D> ();
		initPosX = this.transform.position.x;
		initPosY = this.transform.position.y;
		xPath = new float[]{-1, 1};
		facing = "left";
		moveDirection = new Vector2(1, 0);
	}

	void onTrigger(){
		Debug.Log ("hello");
	}

	void FixedUpdate () {
		float currPos = this.transform.position.x;
		if (facing.Equals("left") && currPos >= xPath[1]) {
			flipRight ();
		}
		else if (facing.Equals("right") && currPos <= xPath[0] ) {
			flipLeft ();
		}
		transform.Translate (moveDirection * speed * Time.deltaTime, Space.World);
	}

	public void flipLeft() {
		Vector2 colliderPos = new Vector2 (0, 0);
		moveDirection.x = 1;
		facing = "left";
		guardSensor.flipRight ();
		sr.flipX = false;
		colliderPos.x = -colliderOffset;
		bc.offset = colliderPos;
	}

	public void flipRight() {
		Vector2 colliderPos = new Vector2 (0, 0);
		moveDirection.x = -1;
		facing = "right";
		guardSensor.flipLeft ();
		sr.flipX = true;
		colliderPos.x = colliderOffset;
		bc.offset = colliderPos;
	}
}