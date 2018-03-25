using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour {

	public float backgroundSize;
	public float parallaxSmoothing;

	private Transform cameraTransform;
	private Transform[] layers;
	private int leftIndex;
	private int rightIndex;
	private float viewZone = 10f;
	private float lastCamera_X;

	// Use this for initialization
	void Start () 
	{
		cameraTransform = Camera.main.transform;
		lastCamera_X = cameraTransform.position.x;
		layers = new Transform[transform.childCount];

		for(int i = 0; i < transform.childCount; i++)
		{
			layers[i] = transform.GetChild(i);
		}

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}
	
	void LateUpdate ()
	{
		float delta_X = cameraTransform.position.x - lastCamera_X;
		transform.position += Vector3.right * (delta_X * parallaxSmoothing);
		lastCamera_X = cameraTransform.position.x;

		//transform.position = cameraTransform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
		{
			ScrollLeft();
		}
		if(cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
		{
			ScrollRight();
		}
	}

	private void ScrollLeft()
	{
		layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
		leftIndex = rightIndex;
		rightIndex--;

		if(rightIndex < 0)
		{
			rightIndex = layers.Length - 1;
		}
	}

	private void ScrollRight()
	{
		layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
		rightIndex = leftIndex;
		leftIndex++;

		if(leftIndex == layers.Length)
		{
			leftIndex = 0;
		}
	}
}
