using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed=5.0f; 
    public float rotationSpeed = 720.0f; //プレイヤーの回転速度
    public float jumpForce = 5.0f; //ジャンプ力
    Rigidbody rb; 
    bool isGrounded = false; //地面判定

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        Jump();
        Rotate();
    }

    void Move(){
        float moveX = Input.GetAxis("Horizontal"); //水平方向のキーの押され具体を取得（ー１か、０か、１かを取得）
        float moveZ = Input.GetAxis("Vertical"); //垂直方向
        Vector3 movement = new Vector3(moveX,0,moveZ) * moveSpeed * Time.deltaTime; 
        //キャラクターを動かす()の中に指定した座標へキャラを向かわせるメソッド
        rb.MovePosition(transform.position + movement);
    }

    void Rotate(){
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")); 
        //magnitudeはVectore型　横縦いずれかに入った力による方向の長さ
        if(direction.magnitude>=0.1f){
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg; 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref rotationSpeed,0.1f); 
            transform.rotation = Quaternion.Euler(0,angle,0); 
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && isGrounded){
             //上方向に力を与える　Impulseは瞬間的に力を与える
            rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse); 
            isGrounded = false; 
            Debug.Log("Jump");
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Ground"){
            isGrounded = true; 
        }
    }
}