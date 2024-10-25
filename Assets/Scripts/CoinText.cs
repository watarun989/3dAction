using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText; 
    public GameObject Player; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = Player.GetComponent<PlayerController>().coin.ToString(); 
    }
}
