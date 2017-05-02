using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : Sensor
{

    public PlayerMovement player;
    GameObject so;
    Destructible sensedObj;
    Vector3 initPos;
    public bool senseDestructible;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        colliderOffset = colliderOffset;
        initPos = new Vector3(colliderOffset, 0, 0);
        senseDestructible = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Destructible")
        {
			so = other.gameObject;
            sensedObj = (Destructible)so.GetComponent(typeof(Destructible));
            //player.sensingDestructible = true;
            senseDestructible = true;
        }
    }

    public GameObject Targetting() {
        if (this.so != null)
        {
            return this.so;
        }
        else
        {
            return null;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        so = null;
        sensedObj = null;
        //player.sensingDestructible = false;
        senseDestructible = false;
    }

    public void Damage(PlayerMovement player)
    {
        sensedObj.TakeDamage(player);
    }

    public bool Destructible()
    {
        return senseDestructible;
    }
}