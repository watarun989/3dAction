using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextSceneName; 
    TimeController timeCnt; 

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GameObject.FindObjectOfType<TimeController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetryScene()
    {
        SceneManager.LoadScene(PlayerController.sceneChoice);
    }

    public void NextScene(){
        SceneManager.LoadScene(nextSceneName); 
        TimeController.totalTime = timeCnt.currentTime; 
    }
}
