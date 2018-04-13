using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockscript : MonoBehaviour {

	public Sprite [] sheet;
	private int minTexture = 0;
	private int maxTexture = 25;

	SpriteRenderer blocks;


	// Use this for initialization
	void Start () {
		blocks = GetComponent<SpriteRenderer> ();
		int random = Random.Range (minTexture, maxTexture);
		blocks.sprite = sheet [random];
	}

	// Update is called once per frame



}
