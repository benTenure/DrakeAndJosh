using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    //public int offSetX = 1;

    // These are used for checking for instantiation
    public bool hasRightBuddy = false;
    public bool hasLeftBuddy = false;

    public bool reverseScale = false;       // Used if object is not tilable

    private float spriteWidth = 0f;         // The width of our element
    private Camera cam;
    private Transform myTransform;

	// Used for referencing
	private void Awake()
	{
        cam = Camera.main;
        myTransform = transform;
	}

	// Use this for initialization
	void Start () 
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Do we still need a buddy?
        if (hasLeftBuddy == false || hasRightBuddy == false) 
        {
            // Calculate half of what the camera can see horizontally (with coordinates)
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            // Calculate the x position where the camera can see the x position of sprite
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtent;

            // Checking if we can see edge of our image
            if (cam.transform.position.x >= edgeVisiblePositionRight /*- offSetX*/ && hasRightBuddy == false)
            {
                MakeNewBuddy(1);
                hasRightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft /*+ offSetX*/ && hasLeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasLeftBuddy = true;
            }
        }
	}

    // Creates a new buddy on the desired side of our image
    void MakeNewBuddy(int direction)
    {
        // Calculating new position for the new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * direction, myTransform.position.y, myTransform.position.z);
        // Instantiating new buddy and storing it in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        // Helps prevent unsightly seams on our tiling images
        if (reverseScale ==true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);

        }

        newBuddy.parent = myTransform.parent;

        if (direction > 0)
        {
            newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
        }
        else 
        {
            newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
        }
    }
}
