using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    //to track the location of the player!
    private Transform playerPos;
    //so we can edit how fast the camera is moving upward
    private float cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //we'll likely just have the camera start at the player's position,
        Vector3 startingPos = transform.position;
        startingPos.y = playerPos.position.y;
        transform.position = startingPos;
        cameraSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //we'll likely just have the camera start at the player's position,
        //and then we'll have it gradually move upward (one unit per second) as the game goes on.
        //so long as the player doesn't die.
        transform.Translate(Vector3.up * Time.deltaTime * cameraSpeed);
        if(playerPos.position.y < (transform.position.y- 20)){
            SceneManager.LoadScene("TestScene2");
        }
        
    }

    //this function gives away the camera's current height, so that the level editor knows
    //when it needs to add more levels!
    public float ReturnHeight(){
        return transform.position.y;
    }

    public void IncreaseSpeed(){
        cameraSpeed += Time.deltaTime * 3;
        Debug.Log(cameraSpeed);
    }
}
