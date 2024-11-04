using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public GameObject eyeImage; 

    // Start is called before the first frame update
    void Start()
    {
        eyeImage.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.eyeStatus){
            eyeImage.SetActive(true); 
        }else{
            eyeImage.SetActive(false); 
        }
    }
}
