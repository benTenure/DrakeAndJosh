using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunny : MonoBehaviour {
    // https://unity3d.com/learn/tutorials/topics/animation/animator-scripting
    private Animator anim;
    private bool facingRight = false;
    private Transform playerTrans; // player transform
    public Rigidbody2D ballPrefab;
    private float lastAttackTime = 0f;

    // projectile stuff https://www.youtube.com/watch?v=KKgtC_Gy65c
    public LayerMask whatToHit;

    void Start() {
        // TODO: add timeout to the balls so that they disappear eventually if you miss
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        lastAttackTime += Time.deltaTime;
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
            {
                ToggleAttack(true);
            }
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

    // do a projectile attack towards the player
    private void attack()
    {

        // source and destination points for the projectile
        Vector2 dest = new Vector2(playerTrans.position.x, playerTrans.position.y);
        Vector2 src = new Vector2(this.transform.position.x, this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(src, dest-src, 5, whatToHit);
        Debug.DrawLine(src, dest);
        Debug.Log("\nattacking from " + src + " to " + dest);
        // note for later: under the dust bunny script in the inpspector, you can choose which layers it can be hit
        // (right now it's just set to player, (I put the player on the player layer in its settings))
        
        // only shoot a projectile if we can "see" the player
        if (hit.collider != null) // something hit
        {
            Debug.DrawLine(src, hit.point, Color.red);
            // create a ball object
            // TODO: consider making the ball not a rigidbody, just have a collider that will trigger it to disappear
            ballPrefab = Instantiate(ballPrefab) as Rigidbody2D;
            ballPrefab.transform.position = transform.position; // spawn at location of dust bunny
            ballPrefab.velocity = 5*(dest-src);
        }
    }

    // control whether the dust bunny is attacking or not
    void ToggleAttack(bool state)
    {
        anim.SetBool("attacking", state);
        // attack with a projectile up to once every 1.5 seconds
        if (lastAttackTime > 1.5)
        {
            attack();
            lastAttackTime = 0;
        }
    }

    // flip the bunny to face the opposite way
    void flipSprite()
    {
        anim.transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

}
