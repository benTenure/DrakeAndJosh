using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunny : MonoBehaviour {
    // https://unity3d.com/learn/tutorials/topics/animation/animator-scripting
    Animator anim;
    float attackTime = 0.0f; // number of seconds to attack for

    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (anim.GetBool("attacking") == true) // if already attacking
        {
            attackTime -= Time.deltaTime;
            if (attackTime < 0)
            {
                ToggleAttack(false);           // stop attacking
            }
        }
    }

   // every time the player collides with the bunny, have the bunny attack for 4 seconds
   // TODO: instead toggle the attack every time the player is close
   // TODO: make the bunny face the player
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("player collided with bunny");
            ToggleAttack(true);     // start attacking
        }
    }

    // control whether the dust bunny is attacking or not
    void ToggleAttack(bool state)
    {
        anim.SetBool("attacking", state);
        if (state)
        {
            attackTime = 4.0f;      // reset the attack timer
        }
        else
        {
            attackTime = 0.0f;
        }

    }
}
