using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destructible : MonoBehaviour {

	//protected Animator anim;
	protected Collider2D myCollider;
	protected bool destroyed = false;
	protected int scoreValue;
	protected int health;

	// Use this for initialization
	public virtual void Start () {
		//anim = GetComponent<Animator>();
		myCollider = GetComponentInChildren<Collider2D>();
	}

	public virtual void FixedUpdate () {

	}

	public int GetScore()
	{
		return scoreValue;
	}

	public int GetHealth() {
		return health;
	}

	public abstract void TakeDamage(PlayerMovement player);
	public abstract void GetDestroyed(PlayerMovement player);
}
