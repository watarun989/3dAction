using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject nextButton; 
    public GameObject statusText; 
    public GameObject eyeImage; 
    public GameObject itemPanel; 

    // Start is called before the first frame update
    void Start()
    {
        nextButton.gameObject.SetActive(false); 
        statusText.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameClear"){
            nextButton.gameObject.SetActive(true); 
            statusText.gameObject.SetActive(true); 
            eyeImage.gameObject.SetActive(false); 
            itemPanel.gameObject.SetActive(false); 
        }
    }
}
