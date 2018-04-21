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
        // TODO: after 0.7 seconds, have this object dissapear
        // TODO: if collision, disappear (if collision is with player then call damage function)
        if (existenceTime > 0.7)
        {
            // TODO: get this working
            //Object.Destroy(this.gameObject);
        }
    }

    public void setVelocity(Vector3 v)
    {
        velocity = v;
        Debug.Log("velocity set to " + velocity);
    }
}
