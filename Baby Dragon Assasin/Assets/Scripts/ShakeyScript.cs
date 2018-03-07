using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeyScript : MonoBehaviour {

	bool Up;
	float timer;

	void OnCollisionEnter2d(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			if (timer <= 2){
				if(Up) {
					transform.Translate(0,0.1f,0);
					Up = false;
				} else {
					transform.Translate(0, 0.1f, 0);
					Up = true;
				}
			}
			timer += Time.deltaTime;
			Debug.Log(timer);
		}
	}
}
