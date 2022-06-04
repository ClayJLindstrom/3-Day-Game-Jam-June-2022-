using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform transform;
    private Rigidbody2D rigidBody; 
    private Collider2D collider;
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
        acceleration = 5;
        collider = GetComponent<BoxCollider2D>();
        feet = GetComponent<CircleCollider2D>();
        maxSpeedReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidBody.velocity.y >= 3f){
            Vector2 newSpeed = rigidBody.velocity;
            newSpeed.y = 3f;
            rigidBody.velocity = newSpeed;
            
            if(maxSpeedReached != true){
                maxSpeedReached = true;

            }
            else {
                maxSpeedReached = false;
            }
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            rigidBody.AddForce(Vector2.right *acceleration);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            rigidBody.AddForce(Vector2.left *acceleration);
        }
        if(Input.GetKey(KeyCode.UpArrow) && feet.IsTouchingLayers(Physics2D.AllLayers) && !maxSpeedReached){
            rigidBody.AddForce(Vector2.up *acceleration * 35);
        }
    }
}
