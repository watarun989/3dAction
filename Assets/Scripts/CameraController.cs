using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        follow(); 
    }

    void follow(){
        gameObject.transform.position = player.transform.position+new Vector3(0,10,-5);
        gameObject.transform.rotation = Quaternion.Euler(45,0,0);
    }
}
