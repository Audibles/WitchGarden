using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject fs;
	FrontSensor frontSensor;
	float moveX;
	float moveY;
	float destroyInput;
	public float speed;
	public int score;
	public bool sensingDestructible;

	// Use this for initialization
	void Start () {
		fs = GameObject.Find("FrontSensor");
		frontSensor = (FrontSensor) fs.GetComponent(typeof(FrontSensor));

		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb.freezeRotation = true;
		score = 0;
		sensingDestructible = false;
	}
	
	// Update is called once per frame
	void Update() {
		destroyInput = Input.GetAxis ("Fire1");
		if (destroyInput > 0 && sensingDestructible) {
			frontSensor.Damage (this);
		}
	}

	void FixedUpdate () {
		// NOTE: atm, this just flips the sprite when the player changes directions.
		// Later, we will replace this with different animations for each direction.
		Vector2 moveDirection = new Vector2(0, 0);
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		if (moveX > 0) { //flip right
			frontSensor.flipRight();
			sr.flipX = false;
			moveDirection.x = 1;
		} else if (moveX < 0) { //flip left
			frontSensor.flipLeft();
			sr.flipX = true;
			moveDirection.x = -1;
		}
		if (moveY > 0) { //flip up
			frontSensor.flipUp();
			sr.flipY = false;
			moveDirection.y = 1;
		} else if (moveY < 0) { //flip down
			frontSensor.flipDown();
			sr.flipY = true;
			moveDirection.y = -1;
		}
			
		transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
	}

	public void getCaught() {
		//this will later actually end the game/cause the guard to chase the player
		Debug.Log("got ya");
		SceneManager.LoadScene("MainScene");
	}
}
