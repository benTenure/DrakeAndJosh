using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeyScript : MonoBehaviour {

    public AnimationClip shakeClip;
    private Animation myAnim;
    private Rigidbody2D rb;
    //int shakeHash = Animator.StringToHash("Platform_Shake");

    public float fallDelay = 2.0f;

	private void Start()
	{
        myAnim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D coll) 
    {
		if (coll.gameObject.tag == "Player") 
        {
            //myAnim.Play

            //anim.SetTrigger(shakeHash);
            //rb.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(FallAfterDelay());
		}
	}

    IEnumerator FallAfterDelay() 
    {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
    }
}
