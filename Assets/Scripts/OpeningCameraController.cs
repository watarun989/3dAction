using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCameraController : MonoBehaviour
{
    int cameraScene = 1; //カメラの位置を数字でコントロール
    bool resetPosition; //ある程度時間が経ったら、カメラの場所をリセットするスイッチ
    Vector3 positionCamera; 

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = true; 
        transform.position = new Vector3(-155,1,51); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,cameraScene * 90,0); 

        if(cameraScene == 1 && resetPosition || cameraScene == 2 && resetPosition || cameraScene == 3 && resetPosition || cameraScene == 4 && resetPosition){
            resetPosition = false; 
        }

        if(transform.position.x > 40.0f){
            cameraScene = 2; 
            resetPosition = true; 
        }

        if(transform.position.z < 10){
            cameraScene = 3; 
            resetPosition = true; 
        }

        if(transform.position.x < -155){
            cameraScene = 4; 
            resetPosition = true; 
        }

        if(transform.position.z > 51){
            cameraScene = 1; 
            resetPosition = true; 
        }

        transform.Translate(Vector3.forward); 
    }
}
