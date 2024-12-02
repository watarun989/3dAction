using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCameraController : MonoBehaviour
{
    int cameraScene = 1; //カメラの位置を数字でコントロール
    bool resetPosition; //ある程度時間が経ったら、カメラの場所をリセットするスイッチ

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraScene == 1 && resetPosition == true){
            transform.position = new Vector3(-230,1,51); 
            transform.rotation = Quaternion.Euler(0,90,0); 
            resetPosition = false; 
        }


        transform.Translate(Vector3.forward); 
    }
}
