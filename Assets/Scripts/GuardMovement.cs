
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject player;
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
	bool alerted; //becomes true when the player enters the warning zone
	bool chasing; //becomes true when the player enters the chase zone
	float warningRadius = 1f;
	float chaseRadius = 0.5f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		bc = gameObject.GetComponent<BoxCollider2D> ();
		initPosX = this.transform.position.x;
		initPosY = this.transform.position.y;
		xPath = new float[]{-1, 1};
		facing = "left";
		moveDirection = new Vector2(1, 0);
		alerted = false;
		chasing = false;
	}

	void Update() {
		int layerMask = 1 << 2; //this is some witchcraft that makes the ray ignore stuff on layer 2
		layerMask = ~layerMask; //^ same
		float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
		bool playerBlocked = Physics2D.Linecast (this.transform.position, player.transform.position, layerMask);
		Debug.Log (playerBlocked);
		Debug.DrawLine (this.transform.position, player.transform.position);
		if (playerBlocked || distanceToPlayer > warningRadius) {
			Debug.Log ("unseen");
			//alerted = false;
			//chasing = false;
		} else if (!playerBlocked) {
			if (distanceToPlayer <= warningRadius && distanceToPlayer > chaseRadius) {
				Debug.Log ("warning");
				//alerted = true;
				//chasing = false;
			} else if (distanceToPlayer <= chaseRadius) {
				Debug.Log ("chasing");
				//chasing = true;
				//alerted = false;
			}
		}
	}

	void FixedUpdate () {
		/*Vector2 vToPlayer = this.transform.position - player.transform.position;
		RaycastHit2D checkPlayer = Physics2D.Raycast(this.transform.position, vToPlayer, warningRadius);
		Debug.DrawLine (this.transform.position, vToPlayer);
		if (checkPlayer.collider) {
			Debug.Log (checkPlayer.collider);
		}*/

		if (!alerted && !chasing) {
			float currPos = this.transform.position.x;
			if (facing.Equals ("left") && currPos >= xPath [1]) {
				flipRight ();
			} else if (facing.Equals ("right") && currPos <= xPath [0]) {
				flipLeft ();
			}
			transform.Translate (moveDirection * speed * Time.deltaTime, Space.World);
		}

	}

	public void flipLeft() {
		moveDirection.x = 1;
		facing = "left";
		sr.flipX = false;
	}

	public void flipRight() {
		moveDirection.x = -1;
		facing = "right";
		sr.flipX = true;
	}
}