using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform transform;
    private Rigidbody2D rigidBody; 
    private Collider2D collider, leftSide, rightSide;
    private CircleCollider2D feet;

    private float speed;
    private float acceleration; 
    private float jumpUpForce;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody2D>();
        speed = 5;
        acceleration = 10;
        collider = GetComponent<BoxCollider2D>();
        //the left and right sides are so that we can tell if this guy is touching a wall or not.
        leftSide = gameObject.transform.Find("LeftSide").GetComponent<BoxCollider2D>();
        rightSide = gameObject.transform.Find("RightSide").GetComponent<BoxCollider2D>();
        feet = GetComponent<CircleCollider2D>();
        jumpUpForce = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //moving right
        //if pressing key, and we're not hitting a wall
        if(Input.GetKey(KeyCode.RightArrow) && !rightSide.IsTouchingLayers(Physics2D.AllLayers)){
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
            rigidBody.velocity = new Vector2(0, 0);
        }
        //jumping
        if((feet.IsTouchingLayers(Physics2D.AllLayers) || BodyPartTouchingWall()) && Input.GetKey(KeyCode.UpArrow)){
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpUpForce);
        }
        else if(leftSide.IsTouchingLayers(Physics2D.AllLayers) && Input.GetKey(KeyCode.RightArrow)){
                Debug.Log("right side");
                rigidBody.velocity = new Vector2(speed, jumpUpForce);
        }
        else if(rightSide.IsTouchingLayers(Physics2D.AllLayers) && Input.GetKey(KeyCode.LeftArrow)){
                Debug.Log("Left side");
                rigidBody.velocity = new Vector2(-speed, jumpUpForce);
        }
    }


    private bool BodyPartTouchingWall(){
        return 
        leftSide.IsTouchingLayers(Physics2D.AllLayers) ||
        rightSide.IsTouchingLayers(Physics2D.AllLayers);
    }
}
