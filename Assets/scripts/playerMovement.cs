using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
	public enum GameType { platformer, endlessRunner };

    public GameType myGame;
    public Transform groundCheckObject;
    public float groundCheckDistance;

    public float jumpForce;
    public float speed;

    bool jump_Enable;

    Rigidbody2D rb; //grabs the object's rigidBody
    float bounceAmt;
    bool beingPushed;
    Vector2 pushDir;
    float pushPower;

    void Start()
    {
        jump_Enable = true;
        beingPushed = false;
        bounceAmt = 10;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("You chose " + myGame + "! Good Luck designer!");
    }



    void FixedUpdate()
    {
        Vector2.ClampMagnitude(rb.velocity, 6.5f);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //ADD A DOUBLE JUMP!!!
            RaycastHit2D hit = Physics2D.Raycast((Vector2)groundCheckObject.position, Vector2.down, groundCheckDistance);
            if (hit.collider == null)
            {
                Debug.Log("nothing");
            }
            else if (hit.collider.tag == "ground")
            {
                if (jump_Enable)
                    rb.AddForce(Vector2.ClampMagnitude(Vector2.up * jumpForce, 6.5f), ForceMode2D.Impulse);
            }
        }
        if (rb.velocity.y > 7)
        {
            Debug.Log(rb.velocity);
            Vector3 pullDown = new Vector3(rb.velocity.x, -2, 0f); //set maximum 
            rb.velocity = pullDown;
        }

        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f);
        rb.velocity = move;

		//If you change the animations it needs to be named properly or the string needs to be changed
		if (x > 0) {
			transform.gameObject.GetComponent<Animator> ().Play ("right");
		}
		if (x < 0) {
			transform.gameObject.GetComponent<Animator> ().Play ("left");
		}
		if (x == 0) {
			transform.gameObject.GetComponent<Animator> ().Play ("idle");
		}
    }

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "collectible") {
            //make it run a function in the object script that runs the following, this allows the player to set the varibles for the objects
            //UI change
            //playsound
            col.gameObject.GetComponent<genericObjects>().playSound();
			Destroy (col.gameObject);
		}
        if(col.gameObject.tag == "enemy")
        {

            Vector3 move = new Vector3(rb.velocity.x, 6, 0f);
            rb.velocity = move;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "pusher")        {   
        
            rb.AddForce(Vector2.ClampMagnitude(transform.up * col.GetComponent<genericObjects>().pushPower, 6.5f), ForceMode2D.Impulse);
        }
	}

    void OnTriggerExit2D(Collider2D col)
    {

    }



}
