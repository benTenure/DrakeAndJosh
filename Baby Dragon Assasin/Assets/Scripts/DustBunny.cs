using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunny : MonoBehaviour {
    // https://unity3d.com/learn/tutorials/topics/animation/animator-scripting
    private Animator anim;
    private bool facingRight = false;
    private Transform playerTrans; // player transform
    public GameObject ballPrefab;  // projectile to throw
    private float lastAttackTime = 0f;
    public float speed;
    public float xDistTrigger;
    public float yDistTrigger;

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
            if (Mathf.Abs(xDist) < xDistTrigger && Mathf.Abs(yDist) < yDistTrigger)
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
        // perhaps keep one ballPrefab hidden so we can keep cloning it (without it destroying itself)
        // source and destination points for the projectile
        Vector2 dest = new Vector2(playerTrans.position.x, playerTrans.position.y);
        Vector2 src = new Vector2(this.transform.position.x, this.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(src, dest-src, 200, whatToHit);
        Debug.DrawLine(src, dest);
        Debug.Log("\nattacking from " + src + " to " + dest);
        // note for later: under the dust bunny script in the inpspector, you can choose which layers it can be hit
        // (right now it's just set to player, (I put the player on the player layer in its settings))
        
        // only shoot a projectile if we can "see" the player
        if (hit.collider != null) // something hit
        {
            Debug.DrawLine(src, hit.point, Color.red);
            // create a ball object
            // https://gamedev.stackexchange.com/questions/98328/instantiate-a-prefab-and-call-a-method-from-its-script
            // https://www.youtube.com/watch?v=Q9xKjShQwsI

            ballPrefab = Instantiate(ballPrefab) as GameObject;
            BallProjectile projectile = ballPrefab.GetComponent<BallProjectile>();
            projectile.transform.position = transform.position; // spawn at location of dust bunny
            projectile.velocity = speed * (dest - src)/(dest-src).magnitude;         // set the velocity variable of the ball (this is called before start() in ball)
        }
    }

    // control whether the dust bunny is attacking or not
    private void ToggleAttack(bool state)
    {
        anim.SetBool("attacking", state);
        // attack with a projectile up to once every 1.5 seconds
        if (state && lastAttackTime > 1.5)
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
