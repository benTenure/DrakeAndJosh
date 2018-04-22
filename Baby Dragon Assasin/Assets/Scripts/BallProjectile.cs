using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour {
    private float existenceTime = 0f;
    public Vector3 velocity; // to be set in the dustbunny script when this object is instantiated

    void Start () {
        //Destroy(this, 2f);
    }
	
	// Update is called once per frame
	void Update () {
        // update the location of the object (because it's not a rigidbody we can't just set its velocity)
        transform.position += velocity * Time.deltaTime;

        existenceTime += Time.deltaTime;
        if (existenceTime > 0.7)
        {
            // TODO: fix problem where we can't instantiate after destroying the first one
            //Object.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("ball collision with " + coll);
        if (coll.tag == "Player")
        {
            HelathSystemScript health = FindObjectsOfType(typeof(HelathSystemScript))[0] as HelathSystemScript; //GameObject.Find("_GameManager").GetComponent<HelathSystemScript>();
            Debug.Log("found " + health);
            health.TakeDamage();
            // call TakeDamage() in health system script
            health.TakeDamage();
        }

        if (coll.tag != "Enemy")
        {
            //Object.Destroy(this.gameObject);
        }
    }
}
