using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWallScript : MonoBehaviour {

    private Vector3 newPos;


	// Use this for initialization
	void Start () 
        {     
                newPos = new Vector3(transform.position.x + 20, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
        {

                //Move one unit in the x-direction
                transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime);

                //After 20 units stop moving

	}
}
