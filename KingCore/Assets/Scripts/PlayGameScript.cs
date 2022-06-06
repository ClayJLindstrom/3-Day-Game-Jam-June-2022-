using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour
{
    public Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(BeginGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginGame(){
        SceneManager.LoadScene("TestScene2");
    }
}
