using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundScript : MonoBehaviour {

	//private float groundHorizontalLength = 11.52f;
	//private float sceneHorizontalLength = 23.04f;				// THIS IS THE X-LENGTH OF THE CAVE. DO NOT CHANGE. EVER.

	public GameObject player;

	private BoxCollider2D groundCollider;
	private float groundHorizontalLength;

	// Use this for initialization
	void Start () 
	{
		groundCollider = GetComponent<BoxCollider2D>();
		groundHorizontalLength = groundCollider.size.x;
		
		// Print value!
		Debug.Log(groundHorizontalLength);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.transform.position.x == groundHorizontalLength)
		{
			RepositionBackground();
		}
	}

	private void RepositionBackground ()
	{
		Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
		transform.position = (Vector2)transform.position + groundOffset;
	}
}
