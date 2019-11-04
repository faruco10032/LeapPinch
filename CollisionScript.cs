using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public float collider_radius = 0.02f;
    public bool collision_flag = false;
    public GameObject collision_object;
    public float R;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(collision_flag);
    }

    void OnTriggerEnter(Collider t){// 接触した瞬間
        if(t.gameObject.tag == "isEnablePinch"){
            collision_flag = true;
        } 
    }

    void OnTriggerStay(Collider t){//なにかに接触している間呼ばれ続ける．接触している物体をtとして格納
        if(collision_flag){
        //接触している物体を取得
        collision_object = t.gameObject;

        //接触している物体の位置をベクトルで取得
        Vector3 t_pos = t.transform.position;

        // 接触している物体と自身の距離を計算
        float distance = Vector3.Distance(this.transform.position, t_pos);
        // Debug.Log("distance : "+ distance);

        // 接触している物体のX軸を取得（R）を取得→のちほど大きさに変更する．rayを使って実装できないかな？
        R = t.transform.localScale.x*0.5f;
        }
    }

    void OnTriggerExit(){//接触判定が外れたら呼ばれる
        if(collision_flag){
            // 物体から離れたら重力を入れる
            Rigidbody rb = collision_object.GetComponent<Rigidbody>();
            rb.useGravity = true;
            
            collision_flag = false;// 物体から離れたらフラグを折る
            collision_object = null;//物体から離れたらGameobjectをクリア
        }
    }
}