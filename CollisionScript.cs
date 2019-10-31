using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public float collider_radius = 0.02f;
    public bool collision_flag = false;
    public GameObject collision_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(collision_flag);
    }

    void OnTriggerStay(Collider t){//なにかに接触している間呼ばれ続ける．接触している物体をtとして格納
        //接触している物体を取得
        collision_object = t.gameObject;

        //接触している物体の位置をベクトルで取得
        Vector3 t_pos = t.transform.position;

        // 接触している物体と自身の距離を計算
        float distance = Vector3.Distance(this.transform.position, t_pos);
        // Debug.Log("distance : "+ distance);

        // 中心間距離に応じて接触判定を更新
        if(distance<collider_radius){
            collision_flag=true;
        }else{
            collision_flag=false;
        }
    }

    void OnTriggerExit(){//接触判定が外れたら呼ばれる
        collision_object = null;//物体から離れたらGameobjectをクリア
    }
}
