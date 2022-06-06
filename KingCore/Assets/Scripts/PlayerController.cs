using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform transform;
    private Rigidbody2D rigidBody; 
    private Collider2D collider, leftSide, rightSide;
    private CircleCollider2D feet;
    //the animator
    private Animator anime;

    private float speed;
    private float acceleration; 
    private float jumpUpForce;
    //for controlling which way the character is facing
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody2D>();
        speed = 5;
        acceleration = 10;
        collider = GetComponent<BoxCollider2D>();

        anime = transform.Find("SpriteManager").GetComponent<Animator>();
        //the left and right sides are so that we can tell if this guy is touching a wall or not.
        leftSide = gameObject.transform.Find("LeftSide").GetComponent<BoxCollider2D>();
        rightSide = gameObject.transform.Find("RightSide").GetComponent<BoxCollider2D>();
        feet = GetComponent<CircleCollider2D>();
        jumpUpForce = 12.5f;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //moving right
        //if pressing key, and we're not hitting a wall
        if(Input.GetKey(KeyCode.RightArrow)){
            facingRight = true;
            if(rigidBody.velocity.x > speed){
                Vector2 newSpeed = rigidBody.velocity;
                newSpeed.x = speed;
                rigidBody.velocity = newSpeed;
            }
            else{
                rigidBody.AddForce(Vector2.right *acceleration);
            }
        }
        //moving left
        //if button is pressed, and left side isn't hitting a wall
        if(Input.GetKey(KeyCode.LeftArrow)){
            facingRight = false;
            if(rigidBody.velocity.x <  -speed){
                Vector2 newSpeed = rigidBody.velocity;
                newSpeed.x = -speed;
                rigidBody.velocity = newSpeed;
            }
            else{
                rigidBody.AddForce(Vector2.left *acceleration);
            }
        }
        if(BodyPartTouchingWall()){
            if(rigidBody.velocity.y < -jumpUpForce/6)
            rigidBody.velocity = new Vector2(0, -jumpUpForce/6);
        }
        //jumping
        if(Input.GetKey(KeyCode.UpArrow)){
            //jumping
            if(feet.IsTouchingLayers(Physics2D.AllLayers)){
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpUpForce);
            }
            //climbing
            else if(BodyPartTouchingWall()){
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpUpForce/6);
            }
        }
        if(feet.IsTouchingLayers(Physics2D.AllLayers)){}
        if(leftSide.IsTouchingLayers(Physics2D.AllLayers) && Input.GetKey(KeyCode.RightArrow)){
                Debug.Log("right side");
                rigidBody.velocity = new Vector2(speed, jumpUpForce*3/5);
        }
        else if(rightSide.IsTouchingLayers(Physics2D.AllLayers) && Input.GetKey(KeyCode.LeftArrow)){
                Debug.Log("Left side");
                rigidBody.velocity = new Vector2(-speed, jumpUpForce*3/5);
        }else if(BodyPartTouchingWall() && Input.GetKey(KeyCode.DownArrow)){
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -jumpUpForce/2);
        }

        //Animation work
        //running
        if ((Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow)) && feet.IsTouchingLayers(Physics2D.AllLayers)){
            anime.SetBool("isRunning", true);
        }
        else{anime.SetBool("isRunning", false);}
        //jumping
        anime.SetFloat("yVelocity", rigidBody.velocity.y);
        //climbing
        if(Input.GetKey(KeyCode.UpArrow) && (BodyPartTouchingWall())){
            anime.SetBool("climbing", true);
        }
        else{
            anime.SetBool("climbing", false);
        }
    }

    void FixedUpdate(){
        if(facingRight && transform.Find("SpriteManager").transform.localScale.x < 0){
            Flip();
        }
        if(!facingRight && transform.Find("SpriteManager").transform.localScale.x > 0){
            Flip();
        }
    }


    private bool BodyPartTouchingWall(){
        return 
        leftSide.IsTouchingLayers(Physics2D.AllLayers) ||
        rightSide.IsTouchingLayers(Physics2D.AllLayers);
    }

    private void Flip(){
        Debug.Log("Flipped");
        transform.Find("SpriteManager").transform.localScale = new Vector3(-transform.Find("SpriteManager").transform.localScale.x, transform.Find("SpriteManager").transform.localScale.y, 1);
    }
}
