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
    private float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody2D>();
        speed = 5;
        acceleration = 5;
        collider = GetComponent<BoxCollider2D>();
        feet = GetComponent<CircleCollider2D>();
        jumpForce = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow)){
            rigidBody.AddForce(Vector2.right *acceleration);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            rigidBody.AddForce(Vector2.left *acceleration);
        }
        if(Input.GetKey(KeyCode.UpArrow) && feet.IsTouchingLayers(Physics2D.AllLayers)){
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }
}
