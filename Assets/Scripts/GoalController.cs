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
            goal.gameObject.SetActive(true); 
            GetComponent<BoxCollider>().enabled = true; 
            goalAnnounce.SetActive(true); 
            // Debug.Log("true"); 
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            goalTouch = true; 
            goal.gameObject.SetActive(false); 
            PlayerController.gameState = "gameClear"; 
        }
    }
}
