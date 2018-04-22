using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour {
    public Vector3 velocity; // to be set in the dustbunny script when this object is instantiated
    private Transform playerTrans; // player transform

    void Start () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        // delete this object after 1.7 seconds
        Destroy(gameObject, 1.7f);
    }
	
	// Update is called once per frame
	void Update () {
        // update the location of the object (because it's not a rigidbody we can't just set its velocity)
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("ball collision with " + coll);
        // if we hit the player (who isn't in stealth mode)
        if (coll.tag == "Player" && !playerTrans.gameObject.GetComponent<PlayerController>().isHiding)
        {
            HelathSystemScript health = FindObjectsOfType(typeof(HelathSystemScript))[0] as HelathSystemScript; //GameObject.Find("_GameManager").GetComponent<HelathSystemScript>();
            health.TakeDamage();
            Object.Destroy(gameObject);
        }

        // delete after hitting something (other than a bunny)
        if (coll.tag != "Enemy" && coll.tag != "Player")
        {
            Object.Destroy(gameObject);
        }
    }
}
