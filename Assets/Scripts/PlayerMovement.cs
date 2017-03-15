using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject fs;
	FrontSensor frontSensor;
	Animator anim;
	float moveX;
	float moveY;
	float destroyInput;
	public float speed;
	public int score;
	public bool sensingDestructible;
    public GameObject hedge;

	// Use this for initialization
	void Start () {
		fs = GameObject.Find("FrontSensor");
		frontSensor = (FrontSensor) fs.GetComponent(typeof(FrontSensor));

		anim = gameObject.GetComponent<Animator> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb.freezeRotation = true;
		score = 0;
		sensingDestructible = false;

        //ability to build hedge fetch it
        hedge = GameObject.Find("ShortHedge");
        print(hedge);
    }

	// Update is called once per frame
	void Update() {
		destroyInput = Input.GetAxis ("Fire1");
		if (destroyInput > 0 && sensingDestructible) {
			frontSensor.Damage (this);
		}
        if (Input.GetKeyDown("h"))
        {
            print("entered h");
            Vector3 offset = new Vector3(0, -.15f, 0);
            var build = Instantiate(hedge, transform.position + offset, transform.rotation);
        }
    }

	void FixedUpdate () {
		// NOTE: atm, this just flips the sprite when the player changes directions.
		// Later, we will replace this with different animations for each direction.
		Vector2 moveDirection = new Vector2(0, 0);
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		if (moveX > 0) { //flip right
			anim.SetBool("isWalkingRight", true);
			frontSensor.flipRight();
			moveDirection.x = 1;
		} else if (moveX < 0) { //flip left
			anim.SetBool("isWalkingLeft", true);
			frontSensor.flipLeft();
			moveDirection.x = -1;
		} else {
			anim.SetBool ("isWalkingRight", false);
			anim.SetBool ("isWalkingLeft", false);
		}
		if (moveY > 0) { //flip up
			//anim.SetBool ("isWalkingRight", true);
			frontSensor.flipUp ();
			moveDirection.y = 1;
		} else if (moveY < 0) { //flip down
			//anim.SetBool ("isWalkingLeft", true);
			frontSensor.flipDown ();
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