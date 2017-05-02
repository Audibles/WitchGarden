using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTower : MonoBehaviour {

	public string type; //either spawn or speed
	bool guardSpawned;
	public GameObject guardClone;
	CircleCollider2D coll;


	void Start() {
		guardSpawned = false;
		coll = gameObject.GetComponent<CircleCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (type.Equals ("speed") && other.gameObject.tag.Equals ("Guard")) {
			GameObject gm = other.gameObject;
			GuardMovement guard = (GuardMovement)gm.GetComponent (typeof(GuardMovement));
			guard.speed *= 1.5f;
		}
		if (type.Equals ("spawn") && other.gameObject.tag.Equals ("Player") && !guardSpawned) {
			guardSpawned = true;
			GameObject gm = Instantiate (guardClone);
			GuardMovement guard = (GuardMovement)gm.GetComponent (typeof(GuardMovement));
			Vector2 tempX = new Vector2 ();
			guard.transform.position = this.transform.position + new Vector3 (0f, -this.coll.radius - 0.1f, 0f);
			guard.path = "vertical";
			guard.yPathStart = this.transform.position.y;
			guard.yPathEnd = this.transform.position.y + 1f;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (type.Equals ("speed") && other.gameObject.tag.Equals ("Guard")) {
			GameObject gm = other.gameObject;
			GuardMovement guard = (GuardMovement)gm.GetComponent(typeof(GuardMovement));
			guard.speed /= 1.5f;
		}
	}
}
