using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxCatch : MonoBehaviour
{
    bool isObject; //木箱に触れているかどうか
    bool isHold; //木箱を掴んでいるかどうか
    public BoxCollider boxCollider; //isTriggerじゃない方のコライダーをアタッチ

    GameObject player; //プレイヤー情報を格納するための変数

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Eキーが押された時
        if(Input.GetKeyDown(KeyCode.E))
        {
            //プレイヤーが木箱に触れていたら & isHoldがまだfalseなら
            if (isObject && isHold == false)
            {
                PickupObject();
            }
            else if(isHold) //isHoldがすでにtrueだった場合 →　箱を持っている
            {
                DropObject();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isObject = true; //プレイヤーが木箱に触れている
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isObject = false; //プレイヤーが木箱に触れている
        }
    }

    void PickupObject()
    {
        isHold = true; //木箱を掴んでいるというフラグをON
        boxCollider.enabled = false;
        transform.SetParent(player.transform); //playerが(this)の親になる
        transform.localPosition = new Vector3(0, 4, 5); //親（プレイヤー）からみて決まった位置にボックスがいく

        GetComponent<Rigidbody>().isKinematic = true; //Rigidbodyの物理演算の影響はなし
        player.GetComponent<PlayerController>().currentMoveSpeed *= 0.8f;
        player.GetComponent<PlayerController>().currentJumpForce = 0;
    }

    void DropObject()
    {
        isHold = false; //プレイヤーが木箱から離れた
        boxCollider.enabled = true;
        transform.SetParent(null); //箱の親は誰でもない
        GetComponent<Rigidbody>().isKinematic = false; //あらためて箱は物理演算の影響を受ける

        player.GetComponent<PlayerController>().currentJumpForce = player.GetComponent<PlayerController>().jumpForce;
        player.GetComponent<PlayerController>().currentMoveSpeed = player.GetComponent<PlayerController>().moveSpeed;
    }

}
