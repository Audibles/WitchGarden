using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    UIManager uiManager;
    Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject fs;
	FrontSensor frontSensor;
    GameObject hm;
    FrontSensor hedgeMaker;
    Animator anim;
	float moveX;
	float moveY;
	float destroyInput;
	public float speed;
	public int score;
	float startTime;
	public bool sensingDestructible;
    public bool hedgeActivated;
    public bool hedgeMakerVisible;
    public bool hedgePlacement;
    public bool currentlyDamaging;
    public GameObject hedge;

	// Use this for initialization
	void Start () {
		startTime = Time.time + 180;
        uiManager = UIManager.uiManager;
        fs = GameObject.Find("FrontSensor");
		frontSensor = (FrontSensor) fs.GetComponent(typeof(FrontSensor));

        hm = GameObject.Find("HedgeMaker");
        hedgeMaker = (FrontSensor)hm.GetComponent(typeof(FrontSensor));

        anim = gameObject.GetComponent<Animator> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb.freezeRotation = true;
		score = 0;
		sensingDestructible = frontSensor.Destructible();
        hedgePlacement = hedgeMaker.Destructible();
        hedgeActivated = false;
        hedgeMakerVisible = false;
        currentlyDamaging = false;

        //ability to build hedge fetch it
        hedge = GameObject.Find("ShortHedge");
    }

	// Update is called once per frame
	void Update() {
		float guiTime = startTime - Time.time;
		int minutes = (int) guiTime / 60;
		int seconds = (int) guiTime % 60;
		Debug.Log (minutes + " " + seconds);

		destroyInput = Input.GetAxis ("Fire1");
        hedgeMaker.GetComponent<Renderer>().enabled = hedgeMakerVisible;
        sensingDestructible = frontSensor.Destructible();
        hedgePlacement = hedgeMaker.Destructible();

        if (destroyInput > 0 && sensingDestructible && !currentlyDamaging)
        {
            currentlyDamaging = true;
            frontSensor.Damage(this);
        } else if (destroyInput == 0) {
            currentlyDamaging = false;
        }

        //Changes the color of the bush based on whether you can place one there
        if (hedgePlacement && hedgeActivated) {
            hedgeMaker.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else {
            hedgeMaker.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        //Place Hedge ability with "h", "h" again to place "g" to cancel
        if (Input.GetKeyDown("h") && !hedgeActivated) {
            hedgeActivated = true;
            hedgeMakerVisible = true;
            
        } else if (Input.GetKeyDown("h") && hedgeActivated) {
            if (!hedgePlacement)
            {
                Vector3 hedgeMakerPosition = hedgeMaker.GetComponent<Transform>().position;
                hedgeMakerPosition.z = 0;
                var build = Instantiate(hedge, hedgeMakerPosition, hedgeMaker.GetComponent<Transform>().rotation);
                //OnTriggerEnter(build.GetComponent<Collider2D>());
                hedgeActivated = false;
                hedgeMakerVisible = false;
            }

        } else if (Input.GetKeyDown("g") && hedgeActivated) {
            hedgeActivated = false;
            hedgeMakerVisible = false;
        }

    }

        void FixedUpdate () {
		// NOTE: atm, this just flips the sprite when the player changes directions.
		// Later, we will replace this with different animations for each direction.
		Vector2 moveDirection = new Vector2(0, 0);
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");

		if (moveX > 0) { //flip right and moving right
			anim.SetBool("isWalkingRight", true);
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isFacingRight", false);
            frontSensor.flipRight();
            hedgeMaker.flipRight();
            moveDirection.x = 50;

        } else if (moveX < 0) { //flip left and moving left
			anim.SetBool("isWalkingLeft", true);
            anim.SetBool("isWalkingRight", false);
            anim.SetBool("isFacingLeft", false);
            frontSensor.flipLeft();
            hedgeMaker.flipLeft();
            moveDirection.x = -50;
        } else { // not moving
            if (anim.GetBool("isWalkingRight")) {
                anim.SetBool("isWalkingRight", false);
                anim.SetBool("isFacingRight", true);
                anim.SetBool("isFacingLeft", false);
            } else if (anim.GetBool("isWalkingLeft")) {
                anim.SetBool("isWalkingLeft", false);
                anim.SetBool("isFacingLeft", true);
                anim.SetBool("isFacingRight", false);
            }
		}
        if (moveY > 0) { //flip up
          anim.SetBool ("isWalkingUp", true);
          anim.SetBool("isWalkingDown", false);
            anim.SetBool("isFacingUp", false);
            frontSensor.flipUp ();
            hedgeMaker.flipUp();
            moveDirection.y = 50;
        } else if (moveY < 0) { //flip down
          anim.SetBool ("isWalkingDown", true);
            anim.SetBool("isWalkingUp", false);
            anim.SetBool("isFacingDown", false);
            frontSensor.flipDown ();
            hedgeMaker.flipDown();
            moveDirection.y = -50;
        } else { // not moving
            if (anim.GetBool("isWalkingUp")) {
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isFacingUp", true);
                anim.SetBool("isFacingDown", false);
            }
            else if (anim.GetBool("isWalkingDown")) {
                anim.SetBool("isWalkingDown", false);
                anim.SetBool("isFacingDown", true);
                anim.SetBool("isFacingUp", false);
            }
        }

		rb.velocity = moveDirection * speed * Time.deltaTime;
	}

	public void getCaught() {
		//this will later actually end the game/cause the guard to chase the player
		Debug.Log("got ya");
        //SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene("MenuScene");
        //uiManager.TakeLife();
    }
}