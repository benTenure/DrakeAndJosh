using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour {
    private float existenceTime = 0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("time: " + existenceTime);
        existenceTime += Time.deltaTime;
        // after 0.7 seconds, have this object dissapear
        if (existenceTime > 0.7)
        {
            // TODO: get this working
            //Object.Destroy(this.gameObject);
        }
    }
}
