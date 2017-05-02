using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    UIManager uiManager;
    Rigidbody2D rb;
	SpriteRenderer sr;
	GameObject fs;
	FrontSensor frontSensor;
    GameObject hm;
    FrontSensor hedgeMaker;
    Animator anim;
    GameObject target;
    float moveX;
	float moveY;
	float destroyInput;
	public float speed;
    
    public int score;

    public bool sensingDestructible;
    private bool hedgeActivated;
    private bool hedgeMakerVisible;
    private bool hedgePlacement;
    public bool currentlyDamaging;
    private GameObject hedge;
    private AudioSource source;
    private bool moving1;
    private bool moving2;

    // Use this for initialization
    void Start () {
		
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

        source = GetComponent<AudioSource>();
        target = frontSensor.Targetting();
        source.Play();
        //ability to build hedge fetch it
        hedge = GameObject.Find("ShortHedge");
    }


    void hitAnimation() {
        if (anim.GetBool("isWalkingDown") || anim.GetBool("isFacingDown")) {
            anim.SetTrigger("HitDown");
        } if (anim.GetBool("isWalkingUp") || anim.GetBool("isFacingUp"))
        {
            anim.SetTrigger("HitUp");
        }
         if (anim.GetBool("isWalkingLeft") || anim.GetBool("isFacingLeft"))
        {
            anim.SetTrigger("HitLeft");
        }
        if (anim.GetBool("isWalkingRight") || anim.GetBool("isFacingRight"))
        {
            anim.SetTrigger("HitRight");
        }

    }


	// Update is called once per frame
	void Update() {
		
        //Debug.Log (minutes + " " + seconds);

        destroyInput = Input.GetAxis ("Fire1");
        hedgeMaker.GetComponent<Renderer>().enabled = hedgeMakerVisible;
        sensingDestructible = frontSensor.Destructible();
        hedgePlacement = hedgeMaker.Destructible();


       if (sensingDestructible)
        {
            GameObject new_target = frontSensor.Targetting();
            if (new_target != target) {
                if (target != null) {
                    //target.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    SpriteOutline oldtargetscript = target.GetComponent<SpriteOutline>();
                    oldtargetscript.setActivate(false);
                }
                target = new_target;
                SpriteOutline outlinescript = target.GetComponent<SpriteOutline>();
                outlinescript.setActivate(true);
                //target.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }

            try {
                if (target.GetComponent<ShortBush>().AtCritHealth())
                {
                    SpriteOutline oldtargetscript = target.GetComponent<SpriteOutline>();
                    oldtargetscript.setActivate(false);
                }
            } catch {
                print("Found a pot!");
            }
            
        }
        else if (target != null) { 
            //target.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            SpriteOutline outlinescript = target.GetComponent<SpriteOutline>();
            outlinescript.setActivate(false);
            target = null;
        }

        
        if (destroyInput > 0 && sensingDestructible && !currentlyDamaging)
        {
            hitAnimation();
            currentlyDamaging = true;
            frontSensor.Damage(this);
        }
        else if (destroyInput == 0)
        {
            currentlyDamaging = false;
        }

        //Changes the color of the bush based on whether you can place one there
        if (hedgePlacement && hedgeActivated)
        {
            hedgeMaker.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            hedgeMaker.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        //Place Hedge ability with "h", "h" again to place "g" to cancel
        if (Input.GetKeyDown("h") && !hedgeActivated)
        {
            hedgeActivated = true;
            hedgeMakerVisible = true;

        }
        else if (Input.GetKeyDown("h") && hedgeActivated)
        {
            if (!hedgePlacement)
            {
                Vector3 hedgeMakerPosition = hedgeMaker.GetComponent<Transform>().position;
                hedgeMakerPosition.z = 0;
                var build = Instantiate(hedge, hedgeMakerPosition, hedgeMaker.GetComponent<Transform>().rotation);
                print(build);
                //OnTriggerEnter(build.GetComponent<Collider2D>());
                hedgeActivated = false;
                hedgeMakerVisible = false;
            }

        }
        else if (Input.GetKeyDown("g") && hedgeActivated)
        {
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

        if (!moving1 && !moving2 && (moveX != 0 || moveY != 0)) {
            source.Play();
        }

        moving1 = true;
        moving2 = true;

        if (moveX > 0)
        { //flip right and moving right
            if (anim.GetBool("isWalkingDown") || anim.GetBool("isWalkingUp"))
            {

            }
            else
            {
                anim.SetBool("isWalkingRight", true);
                anim.SetBool("isWalkingLeft", false);
                anim.SetBool("isFacingRight", false);
            }
            frontSensor.flipRight();
            hedgeMaker.flipRight();
            moveDirection.x = 50;

        }
        else if (moveX < 0)
        { //flip left and moving left
            if (anim.GetBool("isWalkingDown") || anim.GetBool("isWalkingUp"))
            {


            }
            else
            {
                anim.SetBool("isWalkingLeft", true);
                anim.SetBool("isWalkingRight", false);
                anim.SetBool("isFacingLeft", false);
            }
            frontSensor.flipLeft();
            hedgeMaker.flipLeft();
            moveDirection.x = -50;
        }
        else
        { // not moving
            moving1 = false;
            if (anim.GetBool("isWalkingRight"))
            {
                anim.SetBool("isWalkingRight", false);
                anim.SetBool("isFacingRight", true);
                anim.SetBool("isFacingLeft", false);
            }
            else if (anim.GetBool("isWalkingLeft"))
            {
                anim.SetBool("isWalkingLeft", false);
                anim.SetBool("isFacingLeft", true);
                anim.SetBool("isFacingRight", false);
            }
        }
        if (moveY > 0)
        { //flip up
            if (anim.GetBool("isWalkingRight") || anim.GetBool("isWalkingLeft"))
            {

            }
            else
            {
                anim.SetBool("isWalkingUp", true);
                anim.SetBool("isWalkingDown", false);
                anim.SetBool("isFacingUp", false);
            }

            frontSensor.flipUp();
            hedgeMaker.flipUp();
            moveDirection.y = 50;
        }
        else if (moveY < 0)
        { //flip down
            if (anim.GetBool("isWalkingRight") || anim.GetBool("isWalkingLeft"))
            {

            }
            else
            {
                anim.SetBool("isWalkingDown", true);
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isFacingDown", false);
            }

            frontSensor.flipDown();
            hedgeMaker.flipDown();
            moveDirection.y = -50;
        }
        else
        { // not moving
            moving2 = false;
            if (anim.GetBool("isWalkingUp"))
            {
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isFacingUp", true);
                anim.SetBool("isFacingDown", false);
            }
            else if (anim.GetBool("isWalkingDown"))
            {
                anim.SetBool("isWalkingDown", false);
                anim.SetBool("isFacingDown", true);
                anim.SetBool("isFacingUp", false);
            }
        }

        if (!moving1 && !moving2)
        {
            source.Pause();
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