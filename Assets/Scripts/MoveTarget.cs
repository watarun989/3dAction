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

    public Transform enemyPosition0; 
    public Transform enemyPosition1; 
    public Transform enemyPosition2; 
    public Transform enemyPosition3; 

    //巡回ルート担当
    public int route = 0; 
    // Start is called before the first frame update
    void Start()
    {
        //エネミーの最初の位置を取得
        // enemyPosition0 = new Vector3(40,-4,60); 
        // enemyPosition1 = new Vector3(40,-4,-13); 
    }

    // Update is called once per frame
    void Update()
    {
        //常にプレイヤーとエネミーの距離を計算
        float distance = Vector3.Distance(player.transform.position,transform.position); 

        //P1に辿り着けるかどうか
        // if(transform.position == enemyPosition0){
        //     //次を目指す状態にする
        //     route = 1; 
        //     Debug.Log("P0"); 
        // }
        if(distance <= 10.0f){
            //（）に指定した座標に向かう （）の中は座標（Vector3型）
            //プレイヤーの座標に向かう
            agent.SetDestination(player.transform.position); 
        } else{
            //エネミーの初期地点へ向かう
            if(route == 0){
                agent.SetDestination(enemyPosition0.position); 
            }
            //P1に辿り着いて、P2を目指す
            else if(route == 1){
                agent.SetDestination(enemyPosition1.position); 
            }

            else if(route == 2){
                agent.SetDestination(enemyPosition2.position); 
            }

            else if(route == 3){
                agent.SetDestination(enemyPosition3.position); 
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "p0"){
            //次の目的地
            route = 1; 
        }

        if(other.gameObject.tag == "p1"){
            //次の目的地
            route = 2; 
        }

        if(other.gameObject.tag == "p2"){
            //次の目的地
            route = 3; 
        }

        if(other.gameObject.tag == "p3"){
            //次の目的地
            route = 0; 
        }
    }
}
