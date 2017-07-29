using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysControl : System.Object
{
    [Tooltip("Y VELOCITY CONSTRAINT: Limits how fast you can move along the Y axis at any give time")]
    public float yVelocity_Constraint;
    public float xVelocity_Constraint;
    [Tooltip("This determines how much you will bounce off of an enemy when you stomp on them. Set to 0 if you aren't jumping on enemy")]
    public float enemyBounce_Amount;
}



public class playerMovement : MonoBehaviour {
	public enum GameType { platformer };

    public GameType myGame;
    public Transform groundCheckObject;
    public float groundCheckDistance;

    public float jumpForce;
    public float speed;

    [Tooltip("The number of times you can collide with an enemy")]
    public int HitPoints;

    public PhysControl physControl;
    Rigidbody2D rb; //grabs the object's rigidBody

    public AudioClip playerHurt;
    AudioSource dio;
    Vector2 pushDir;
    float pushPower;


    void Awake()
    {
        this.gameObject.SetActive(true);
    }
    void Start()
    {
        dio = GetComponent<AudioSource>();
        
        rb = GetComponent<Rigidbody2D>();
        

        //set player health and associated UI
        gameManager.instance.text_PlayerHealth.text = "HP: " + HitPoints;
    }



    void FixedUpdate()
    {
        Debug.DrawRay(groundCheckObject.position, Vector3.down * groundCheckDistance, Color.red);

       
        Vector2.ClampMagnitude(rb.velocity, 6.5f);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //ADD A DOUBLE JUMP!!!
          
            RaycastHit2D hit = Physics2D.Raycast((Vector2)groundCheckObject.position, Vector2.down, groundCheckDistance);
            if (hit.collider == null)
            {
               
            }
            else if (hit.collider.tag == "ground")
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        //jump contrainer - keeps player from flying to far too fast
        if (rb.velocity.y > physControl.yVelocity_Constraint)
        {         
            Vector3 pullDown = new Vector3(rb.velocity.x, physControl.yVelocity_Constraint, 0f);            
            rb.velocity = pullDown;         
        }
        if(myGame == GameType.platformer)
            platformer_Controls();
    }


    void platformer_Controls()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f);
        rb.velocity = move;
       

        //If you change the animations it needs to be named properly or the string needs to be changed
        if (x > 0)
        {
            transform.gameObject.GetComponent<Animator>().Play("right");
        }
        if (x < 0)
        {
            transform.gameObject.GetComponent<Animator>().Play("left");
        }
        if (x == 0)
        {
            transform.gameObject.GetComponent<Animator>().Play("idle");
        }
    }

   
    //Checks if collider(s) attached to this object overalp with another collider(s)
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "collectible") {
            //make it run a function in the object script that runs the following, this allows the player to set the varibles for the objects
            
            gameManager.instance.current_Collect += 1;

            gameManager.instance.collectUI.text = "x" + gameManager.instance.current_Collect;
            //playsound
            //col.gameObject.GetComponent<genericObjects>().playSound();

            col.gameObject.SetActive(false);
		}

        if(col.gameObject.tag == "enemy")
        {
            gameManager.instance.current_Enemy++;
            if(col.GetComponent<badGuy>().badGuySound != null)
            {
                dio.PlayOneShot(col.GetComponent<badGuy>().badGuySound);
            }
                
            Vector3 move = new Vector3(rb.velocity.x, physControl.enemyBounce_Amount, 0f);
            rb.velocity = move;
            col.gameObject.SetActive(false);
        }

        if(col.gameObject.tag == "pusher"){    
            //if col.sound != null
                //play sound       
            rb.AddForce(transform.up * col.GetComponent<genericObjects>().pushPower, ForceMode2D.Impulse);
        }

        if(col.gameObject.tag == "powerUp")
        {
            HitPoints++;
            if(col.GetComponent<genericObjects>().mySound != null)
                dio.PlayOneShot(col.GetComponent<genericObjects>().mySound);
            col.gameObject.SetActive(false);
            gameManager.instance.text_PlayerHealth.text = "HP: " + HitPoints;
        }

        if(col.gameObject.tag == "victory")
        {
            gameManager.instance.runWin();
        }
	}

    //Checks for physical collision between the 2D collider(s) attached to the object
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            HitPoints--;
            gameManager.instance.text_PlayerHealth.text = "HP: " + HitPoints;
            //ContactPoint2D contact = col.contacts[0];
            if (playerHurt != null)
                dio.PlayOneShot(playerHurt);
            if(HitPoints <= 0)
            {
                gameManager.instance.runLost();
            }
        }

    }
}


