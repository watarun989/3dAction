using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextSceneName; 
    TimeController timeCnt;
    public bool isLastScene;

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
        //もしもisLastSceneフラグがONであれば最終ステージだったと判断
        if (isLastScene)
        {
            //もしも現記録がベストスコアより早かったら
            if (TimeController.totalTime < PlayerPrefs.GetFloat("TimeScore"))
            {
                PlayerPrefs.SetFloat("TimeScore", TimeController.totalTime);
            }
        }
    }
}
