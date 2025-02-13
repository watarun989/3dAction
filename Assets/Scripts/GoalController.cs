using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GoalController : MonoBehaviour
{
    public GameObject player; 
    public GameObject goal; 
    bool goalTouch; 
    public GameObject goalAnnounce;

    public TimeController timeCnt;

    public AudioSource audio;
    public AudioSource soundManager; 
    public AudioClip gameClearClip;
    public AudioClip goalAnnounceClip; 

    bool isGoalAnnounce; 

    // Start is called before the first frame update
    void Start()
    {
        goal.gameObject.SetActive(false); 
        goalAnnounce.SetActive(false); 
        GetComponent<BoxCollider>().enabled = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(goalTouch){
            return; 
        }

        if(player.GetComponent<PlayerController>().coin == 3){
            if(!isGoalAnnounce){
                audio.PlayOneShot(goalAnnounceClip); 
                goal.gameObject.SetActive(true); 
                GetComponent<BoxCollider>().enabled = true; 
                goalAnnounce.SetActive(true); 
                // Debug.Log("true"); 
                isGoalAnnounce = true; 
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            goalTouch = true; 
            goal.gameObject.SetActive(false);
            //Timeカウントが無効にするための処置
            timeCnt.isTimeCount = false;

            soundManager.Stop();
            soundManager.clip = gameClearClip;
            soundManager.Play();

            PlayerController.gameState = "gameClear"; 
        }
    }
}
