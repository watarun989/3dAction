using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    public float moveSpeed=5.0f; 
    public float rotationSpeed = 720.0f; //プレイヤーの回転速度
    public float jumpForce = 5.0f; //ジャンプ力
    Rigidbody rb; 
    bool isGrounded = false; //地面判定
    public int coin = 0; //アイテムの数

    public Camera mainCamera; //見下ろしカメラ
    public Camera PersonalCamera; //主観カメラ

    public static bool eyeStatus; //見つかったかどうかのフラグ

    bool isHold; //木箱を掴んでいるかどうか
    //bool isObject; //木箱に触れているかどうか

    GameObject box; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        mainCamera.enabled = true; 
        PersonalCamera.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(eyeStatus);

        Rotate();
        
        //フィジクスクラスの中のレイキャスト命令で足元を探索
        isGrounded = Physics.Raycast(
            transform.position, //センサーの発信位置
            Vector3.down, //センサーの発信する方向
            out RaycastHit hit, //ヒットした相手(変数名hit)を出力(out)する
            2.0f //どのくらいセンサーを伸ばすか
            ) ;

        //Debug.Log("isGrounded:" + isGrounded);

        if(Input.GetKey(KeyCode.Space)){
            mainCamera.enabled = false; 
            PersonalCamera.enabled = true; 
        }else{
            mainCamera.enabled = true; 
            PersonalCamera.enabled = false; 
            Move(); 
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.E)){
            if(isHold){
                DropObject(); 
            }else if(box != null){
                PickupObject(); 
            }
        }
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
            //isGrounded = false; 
            Debug.Log("Jump");
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "item"){
            coin++; 
            Debug.Log("coin" + coin); 
            Destroy(other.gameObject); 
        }
    }

    void OnCollisionEnter(Collision other){
        Debug.Log("Collison"); 
        if(other.gameObject.tag == "enemy"){
            SceneManager.LoadScene("GameOverScene"); 
        }

        if(other.gameObject.tag == "box"){
            //isObject = true;
            box = other.gameObject;//対象のゲームオブジェクト
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "box"){
            //isObject = false;
            box = null; //対象のゲームオブジェクトはいない
        }
    }

    void DropObject(){
        isHold = false; 
        box.transform.SetParent(null); 
        box.GetComponent<Rigidbody>().isKinematic = false; 
        //isObject = false;
        box = null;
    }

    void PickupObject(){
        isHold = true; 
        box.transform.SetParent(transform); 
        box.transform.localPosition = new Vector3(0,4,3.5f); 
        box.GetComponent<Rigidbody>().isKinematic = true; 
    }

    //void OnCollisionEnter(Collision other){
    //    if(other.gameObject.tag == "Ground"){
    //        isGrounded = true; 
    //    }
    //}
}