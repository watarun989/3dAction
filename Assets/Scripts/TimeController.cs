using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TimeController : MonoBehaviour
{
    public static float totalTime = 0; 
    public float currentTime; 
    public bool isTimeCount; 
    public GameObject timeText; 

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeCount){
            currentTime += Time.deltaTime; 
            // totalTime = currentTime; 
            timeText.GetComponent<TextMeshProUGUI>().text = currentTime.ToString("F1") + "s"; 
        }
    }
}
