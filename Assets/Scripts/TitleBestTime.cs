using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TitleBestTime : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestTimeText.text = "Best Time = " + PlayerPrefs.GetFloat("TimeScore") + "s"; 

    }
}
