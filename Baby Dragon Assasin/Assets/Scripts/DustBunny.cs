using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunny : MonoBehaviour {
    // https://unity3d.com/learn/tutorials/topics/animation/animator-scripting
    private Animator anim;
    private bool facingRight = false;
    private Transform playerTrans; // player transform

    void Start() {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        // check distance from player
        if (playerTrans)
        {
            float xDist = this.transform.position.x - playerTrans.position.x;
            float yDist = this.transform.position.y - playerTrans.position.y;
            // face the player
            if (xDist > 0 && facingRight || xDist < 0 && !facingRight)
                flipSprite();

            // attack if the player is too close
            if (Mathf.Abs(xDist) < 5f && Mathf.Abs(yDist) < 1f)
                ToggleAttack(true);
            else
                ToggleAttack(false);

        }
    }

    // TODO: add some way for the player to kill the bunny
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

        }
    }

    // control whether the dust bunny is attacking or not
    void ToggleAttack(bool state)
    {
        anim.SetBool("attacking", state);
    }

    // flip the bunny to face the opposite way
    void flipSprite()
    {
        anim.transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
}
