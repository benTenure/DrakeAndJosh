using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

	[SerializeField] private Sprite[] sheet = new Sprite[26];

	private int minTexture = 0;
	private int maxTexture = 25;

	SpriteRenderer m_SpriteRenderer;


	// Use this for initialization
	void Start () {
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		int randomInt = Random.Range(minTexture, maxTexture);
		m_SpriteRenderer.sprite = sheet[randomInt];


	}
}
