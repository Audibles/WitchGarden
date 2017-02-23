using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	bool touchingWall;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called after Update once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}

	void onCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Wall") {
			
		}
	}
}
