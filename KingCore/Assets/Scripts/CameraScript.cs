using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    public Leaderboard leaderboard;
    //to track the location of the player!
    private Transform playerPos;
    //so we can edit how fast the camera is moving upward
    private float cameraSpeed;
    //rate at which the speed increases
    private float Acceleration = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //we'll likely just have the camera start at the player's position,
        Vector3 startingPos = transform.position;
        startingPos.y = playerPos.position.y;
        transform.position = startingPos;
        cameraSpeed = 1;

        Debug.Log(PlayerPrefs.GetString("playerName"));
    }

    // Update is called once per frame
    void Update()
    {
        //we'll likely just have the camera start at the player's position,
        //and then we'll have it gradually move upward (one unit per second) as the game goes on.
        //so long as the player doesn't die.
        transform.Translate(Vector3.up * Time.deltaTime * cameraSpeed);
        if(playerPos.position.y < (transform.position.y- 15)){
            //reveal the time
            Debug.Log("Final time: " + Time.time);
            StartCoroutine(UploadScore(30));
            SceneManager.LoadScene("Login&LeaderBoard");
            
        }
        
    }

    //this function gives away the camera's current height, so that the level editor knows
    //when it needs to add more levels!
    public float ReturnHeight(){
        return transform.position.y;
    }

    public void IncreaseSpeed(){
        cameraSpeed += Acceleration;
    }

    IEnumerator UploadScore(int score){
    // send time to server:
    yield return leaderboard.SubmitScoreRoutine(37);

}
}


