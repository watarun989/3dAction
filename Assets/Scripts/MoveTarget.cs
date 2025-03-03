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

    public Transform[] enemyPositions; 

    //巡回ルート担当
    public int route = 0;

    //プレイヤーを見つけたかどうかフラグ
    bool isSearchPlayer;

    public float viewAngle = 60.0f; //エネミーの視野の合計
    public float viewDistance = 50.0f; //エネミーの視野の限界

    public AudioSource audio; 
    public AudioClip foundClip; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameClear"){
            agent.ResetPath(); 
            return; 
        }

        //もしプレイヤーが視界に移れば
        if(CanSeePlayer()){
            GetComponent<AudioSource>().PlayOneShot(foundClip); 
            agent.SetDestination(player.transform.position); //追う対象をplayerのポジションにする
            isSearchPlayer = true; //プレイヤーを見つけたフラグ
            PlayerController.eyeStatus = true;//プレイヤーを見つけたアイコンを表示
        } else{

            //プレイヤーを見つけたかどうかのフラグ
            isSearchPlayer = false;
            PlayerController.eyeStatus = false;

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

            Routing(); 
        }
    }
    //プレイヤーが視界に写ればTrue、映ってなければFalseを返すメソッド
    bool CanSeePlayer(){
        //プレイヤーとエネミーの方向の差
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized; 

        //今、エネミーが向いている正面からどのくらいの方角にプレイヤーがいるのか
        float angleToPlayer = Vector3.Angle(transform.forward,directionToPlayer); 

        //視野を２で割った数（３０度）よりプレイヤーの方角（絶対値なのでマイナスは考えず）が小さければ
        //視野に入っている可能性
        if(angleToPlayer < viewAngle / 2){
            //光のセンサーを伸ばして、ぶつかったものがあればTrue
            if(Physics.Raycast(transform.position,directionToPlayer,out RaycastHit hit,viewDistance)){
                //ぶつかった相手がプレイヤーならTrue
                if(hit.collider.gameObject.tag == "Player"){
                    return true; 
                }
            }
        }

        //プレイヤーが視野にいなかった
        return false; 
    }

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

    void OnDrawGizmos()
    {
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
    }
}