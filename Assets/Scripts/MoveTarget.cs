using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class MoveTarget : MonoBehaviour
{
    //NavMeshAgentコンポーネントをagentと言う名前で使う
    public NavMeshAgent agent; 

    //追う対象　変数名player
    public GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //常にプレイヤーとエネミーの距離を計算
        float distance = Vector3.Distance(player.transform.position,transform.position); 
        if(distance <= 10.0f){
            //（）に指定した座標に向かう （）の中は座標（Vector3型）
            //プレイヤーの座標に向かう
            agent.SetDestination(player.transform.position); 
        }
    }
}
