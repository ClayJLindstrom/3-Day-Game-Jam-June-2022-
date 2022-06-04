using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    private Transform transform;
    private Rigidbody2D rigidBody; 
    private Collider2D collider, leftSide, rightSide;
    private CircleCollider2D feet;

    private float speed;
    private float acceleration; 
    private bool maxSpeedReached;
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
        maxSpeedReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidBody.velocity.y >= 3f){
            /*Vector2 newSpeed = rigidBody.velocity;
            newSpeed.y = 3f;
            rigidBody.velocity = newSpeed;*/
            
            if(maxSpeedReached != true){
                //maxSpeedReached = true;

            }
            else {
                maxSpeedReached = false;
            }
        }
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
        //Jumping/Wall Jumping
        if(Input.GetKeyDown(KeyCode.UpArrow) /*&& feet.IsTouchingLayers(Physics2D.AllLayers) && !maxSpeedReached*/){
            //jumping
            Vector2 newSpeed = rigidBody.velocity;
            if(feet.IsTouchingLayers(Physics2D.AllLayers)){
                newSpeed.y = speed * 3;
                /*if(rigidBody.velocity.y > speed * 2){
                    newSpeed.y = speed * 2;
                }
                else{
                    rigidBody.AddForce(Vector2.up * acceleration * 30);
                }*/
            }
            else if(rightSide.IsTouchingLayers(Physics2D.AllLayers)){
                Debug.Log("right side");
                newSpeed.y = speed * 1.5f;
                /*if(rigidBody.velocity.y > speed * 2){
                    newSpeed.y = speed * 2;
                }
                else*/ if(rigidBody.velocity.x < -speed){
                    newSpeed.x = -speed;
                }
                else{
                    rigidBody.AddForce(new Vector2(-1,0) * acceleration* 30);
                }
            }
            else if(leftSide.IsTouchingLayers(Physics2D.AllLayers)){
                Debug.Log("Left side");
                newSpeed.y = speed * 1.5f;
                /*if(rigidBody.velocity.y > speed * 2){
                    newSpeed.y = speed * 2;
                }
                else*/ if(rigidBody.velocity.x > speed){
                    newSpeed.x = speed;
                }
                else{
                    rigidBody.AddForce(new Vector2(1,0) * acceleration* 30);
                }
            }
            rigidBody.velocity = newSpeed;
        }
    }
}
