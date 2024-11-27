using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public GameObject player; 
    public GameObject goal; 
    bool goalTouch; 

    // Start is called before the first frame update
    void Start()
    {
        goal.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if(goalTouch){
            return; 
        }

        if(player.GetComponent<PlayerController>().coin == 1){
            goal.gameObject.SetActive(true); 
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
