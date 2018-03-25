using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

    public Transform[] backgrounds;         // Array of everything that will be parallaxed
    public float smoothing;                 // How smoothly the effect will happen

    private float[] parallaxScales;         // The proportion of the camera's movement compared to background
    private Transform cam;                  // Reference the main camera transform
    private Vector3 previousCamPos;    // Store the camera's previous position in previous frame

    // Called before Start(). Great for references to other game objects.
	private void Awake()
	{
        cam = Camera.main.transform;
	}

	// Use this for initialization
	private void Start()
    {
        // The previous frame at the current frame's camera position
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

	private void Update()
	{
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
	}
}