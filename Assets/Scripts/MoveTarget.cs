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

    // public Transform enemyPosition0; 
    // public Transform enemyPosition1; 
    // public Transform enemyPosition2; 
    // public Transform enemyPosition3; 
    // public Transform enemyPosition4; 
    // public Transform enemyPosition5; 

    public Transform[] enemyPositions; 

    //巡回ルート担当
    public int route = 0;

    //プレイヤーを見つけたかどうかフラグ
    bool isSearchPlayer;


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
        if(PlayerController.gameState == "gameClear"){
            agent.ResetPath(); 
            return; 
        }

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

            //プレイヤーを見つけたフラグ
            isSearchPlayer = true;

            //プレイヤーを見つけたアイコンを表示
            PlayerController.eyeStatus = true;

        } else{

            //プレイヤーを見つけたかどうかのフラグ
            isSearchPlayer = false;
            PlayerController.eyeStatus = false;

            //もしも他のエネミーがプレイヤーを見つけていれば
            //eyeStatusはtrueのまま維持したい

            //FindObjectsOfTypeで探してきたMoveTargetプログラムを持っている
            //オブジェクト全員を配列mtに入れる

            //foreachとは、特定の取得した配列の中身に対して、順に同じ処理を強制する
            //FindObjectsOfType<○○>()　⇒　○○スクリプトを持っているオブジェクトを全員探して　in　の前の配列に詰みこむ
            foreach (MoveTarget mt in FindObjectsOfType<MoveTarget>())
            {
                if(mt.isSearchPlayer == true)
                {
                    PlayerController.eyeStatus = true;
                    break; //ひとり見つかったからforeachのループを抜ける
                }
            }

            //エネミーの初期地点へ向かう
            // if(route == 0){
            //     agent.SetDestination(enemyPosition0.position); 
            // }
            // //P1に辿り着いて、P2を目指す
            // else if(route == 1){
            //     agent.SetDestination(enemyPosition1.position); 
            // }

            // else if(route == 2){
            //     agent.SetDestination(enemyPosition2.position); 
            // }

            // else if(route == 3){
            //     agent.SetDestination(enemyPosition3.position); 
            // }

            // else if(route == 4){
            //     agent.SetDestination(enemyPosition4.position); 
            // }

            // else if(route == 5){
            //     agent.SetDestination(enemyPosition5.position); 
            // }

            Routing(); 
        }
    }

    //プレイヤーを見つけていない時に巡回するメソッド
    void Routing(){
        //もしもルートポイントが１個も配列にセットされてなければ
        //return→すぐメソッドを止める
        if(enemyPositions.Length == 0) return; 

        agent.SetDestination(enemyPositions[route].position); 

        //もしもEnemyと今目指しているポイント(enemyPositions[route])のpositionの差が1.0f以下なら
        //Enemyがポイントにたどり着いたと判断
        if(Vector3.Distance(transform.position,enemyPositions[route].position) <= 1.0f){
            //%はあまり計算→A%Bのあまりの数を求める
            route = (route + 1) % enemyPositions.Length;
        } 
    }

    // void OnTriggerEnter(Collider other){
    //     if(other.gameObject.tag == "p0"){
    //         //次の目的地
    //         route = 1; 
    //     }

    //     if(other.gameObject.tag == "p1"){
    //         //次の目的地
    //         route = 2; 
    //     }

    //     if(other.gameObject.tag == "p2"){
    //         //次の目的地
    //         route = 3; 
    //     }

    //     if(other.gameObject.tag == "p3"){
    //         //次の目的地
    //         route = 4; 
    //     }

    //     if(other.gameObject.tag == "p4"){
    //         //次の目的地
    //         route = 5; 
    //     }

    //     if(other.gameObject.tag == "p5"){
    //         //次の目的地
    //         route = 0; 
    //     }
    // }
}
