using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{

    Rigidbody2D rb;
    SpriteRenderer sr;
    GameObject player;
    public float speed;
    public float xPathStart, xPathEnd;
    public float yPathStart, yPathEnd;
    public string path;
    float initY;
    float initX;
    float range;
    public string facing;
    Vector2 moveDirection;
    Vector2 initPos;
    BoxCollider2D bc;
    float colliderOffset = -0.207f;
    bool alerted; //becomes true when the player enters the warning zone
    bool chasing; //becomes true when the player enters the chase zone
    float warningRadius = 1.5f; //how close the player has to be before the guard goes into slow alert mode
    float chaseRadius = 0.7f; //how close the player has to be before the guard goes into super speedy chase mode
	float wanderRadius = 2f; //how far the guard will "wander" before returning to initPos
    float collisionTime;
    float errorMargin = 0.2f;
    private AudioSource source;

    Animator anim;
    bool curr_direction; // sets what direction guard is facing

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        initPos = this.transform.position;
        initY = initPos.y;
        initX = initPos.x;

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isFacingFront", true);
        source = GetComponent<AudioSource>();

        if (path.Equals("horizontal"))
        {
            moveDirection = new Vector2(1, 0);
            facing = "left";
        }
        else if (path.Equals("vertical"))
        {
            moveDirection = new Vector2(0, 1);
            facing = "up";
        }
        alerted = false;
        chasing = false;
        collisionTime = 5f;
    }

    void Update()
    {
        int layerMask = 1 << 2; //this is some witchcraft that makes the ray ignore stuff on layer 2
        layerMask = ~layerMask; //^ same
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
		float distanceToStart = Vector2.Distance(this.transform.position, this.initPos);
		bool startBlocked = Physics2D.Linecast(this.transform.position, this.initPos, layerMask);
        bool playerBlocked = Physics2D.Linecast(this.transform.position, player.transform.position, layerMask);
		if (distanceToPlayer > warningRadius || distanceToStart > wanderRadius)
        {
            alerted = false;
            chasing = false;
        }
        else if (playerBlocked || startBlocked)
        {
            if (collisionTime <= 0)
            {
                alerted = false;
                chasing = false;
            }
        }
        else if (!playerBlocked)
        {
            if (distanceToPlayer <= warningRadius && distanceToPlayer > chaseRadius)
            {
                alerted = true;
                chasing = false;
            }
            else if (distanceToPlayer <= chaseRadius)
            {
                chasing = true;
                alerted = false;
            }
        }
    }

    void FixedUpdate()
    {

        if (anim.GetBool("isMoving") && !source.isPlaying)
        {
            source.Play();
        }
        else if (!anim.GetBool("isMoving")) {
            source.Stop();
        }

        if (!alerted && !chasing)
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isFacingFront", true);

            if (!onPath())
            {
                // return to path
                Vector3 prev_position = transform.position;
                transform.position = (Vector2)transform.position + (((Vector2)initPos - (Vector2)transform.position).normalized)
                    * speed * Time.deltaTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                // patrol along path
                if (path.Equals("horizontal"))
                {
                    float currPos = this.transform.position.x;
                    if (facing.Equals("left") && currPos >= xPathEnd)
                    {
                        flipRight();
                    }
                    else if (facing.Equals("right") && currPos <= xPathStart)
                    {
                        flipLeft();
                    }
                }
                else if (path.Equals("vertical"))
                {
                    float currPos = this.transform.position.y;
                    if (facing.Equals("up") && currPos >= yPathEnd)
                    {
                        flipDown();
                    }
                    else if (facing.Equals("down") && currPos <= yPathStart)
                    {
                        flipUp();
                    }
                }
                transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            }
        }
        else if (alerted)
        {
            // walk after player slowly
            Vector3 to_add = (player.transform.position - transform.position).normalized
                * speed * Time.deltaTime;
            //print("TOADD");
            //print(to_add);
            Vector3 abs_pos = player.transform.position - transform.position;
            Vector3 prev_position = transform.position;
            transform.position += to_add;
            animationcontroller(abs_pos);
            rb.velocity = Vector3.zero;
        }
        else if (chasing)
        {
            // chase after player quickly
            Vector3 prev_position = transform.position;
            Vector3 to_add = (player.transform.position - transform.position).normalized
                * speed * 2.5f * Time.deltaTime;
            //print("TOADD");
            //print(player.transform.position - transform.position);
            //print(player.transform.position - transform.position.normalized);

            Vector3 abs_pos = player.transform.position - transform.position;

            transform.position += to_add;
            animationcontroller(abs_pos);
            rb.velocity = Vector3.zero;
        }
    }

    private void animationcontroller(Vector3 transform)
    {
        anim.SetBool("isMoving", true);

        //move left/right
        if (Mathf.Abs(transform.x) > Mathf.Abs(transform.y)) {
            anim.SetBool("isWalkingBackward", false);
            anim.SetBool("isWalkingForward", false);

            if (transform.x > 0)
            {
                anim.SetBool("isWalkingRight", true);
                anim.SetBool("isWalkingLeft", false);
            }
            else if (transform.x < 0) {
                anim.SetBool("isWalkingRight", false);
                anim.SetBool("isWalkingLeft", true);

            } 
        }
        //move up/down
        else if (Mathf.Abs(transform.x) < Mathf.Abs(transform.y)) {
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
            if (transform.y > 0)
            {
                anim.SetBool("isWalkingForward", false);
                anim.SetBool("isWalkingBackward", true);
            }
            else if (transform.y < 0)
            {
                anim.SetBool("isWalkingBackward", false);
                anim.SetBool("isWalkingForward", true);
            }
        }
    }
    public bool onPath()
    {
        if (path.Equals("horizontal"))
        {
            return this.transform.position.x >= xPathStart - errorMargin && this.transform.position.x <= xPathEnd + errorMargin
            && this.transform.position.y >= initY - errorMargin && this.transform.position.y <= initY + errorMargin;
        }
        else if (path.Equals("vertical"))
        {
            return this.transform.position.y >= yPathStart - errorMargin && this.transform.position.y <= yPathEnd + errorMargin
                && this.transform.position.x >= initX - errorMargin && this.transform.position.x <= initX + errorMargin;
        }
        return false;
    }

    public void flipLeft()
    {
        moveDirection.x = 1;
        facing = "left";
    }

    public void flipRight()
    {
        moveDirection.x = -1;
        facing = "right";
    }

    public void flipUp()
    {
        moveDirection.y = 1;
        facing = "up";
    }

    public void flipDown()
    {
        moveDirection.y = -1;
        facing = "down";
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement playerColl = (PlayerMovement)player.GetComponent(typeof(PlayerMovement));
            playerColl.getCaught();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "GuardTower")
        {
            collisionTime -= Time.deltaTime;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "GuardTower")
        {
            collisionTime = 5f;
        }
    }
}