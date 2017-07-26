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
    Rigidbody2D rb;

    

    void Start()
    {
       

        rb = GetComponent<Rigidbody2D>();
        Debug.Log("You chose " + myGame + "! Good Luck designer!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)groundCheckObject.position, Vector2.down, groundCheckDistance);
            if(hit.collider.tag == "ground")
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }  
        }
    }

    void FixedUpdate()
    {
        

        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f);
        rb.velocity = move;
    }
}
