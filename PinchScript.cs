using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchScript : MonoBehaviour
{
    public GameObject thumb;
    public GameObject index;
    CollisionScript thumb_script;
    CollisionScript index_script;

    // Start is called before the first frame update
    void Start(){
        //Gameobjectのスクリプトコンポーネントを取得
        thumb_script = thumb.gameObject.GetComponent<CollisionScript>();
        index_script = index.gameObject.GetComponent<CollisionScript>();
        Debug.Log("Start!");
    }

    void Update(){
        // 球の接触判定を取得
        if(thumb_script.collision_flag&&index_script.collision_flag){//親指と人差し指両方で触れているとき
            // Debug.Log("pinch now");
            //指の球体コライダーの位置をベクトルで取得
            Vector3 thumb_pos = thumb.transform.position;
            Vector3 index_pos = index.transform.position;
            //2点間の中心を計算
            Vector3 centor_pos = (thumb_pos+index_pos)*0.5f;

            // 座標を示す空のGameobjectがなければ2点間の中心座標に空のGameobjectを作り，親指と触れている物体をその子にする
            GameObject obj = GameObject.Find("pinch_object_position");
            if(obj==null){
                GameObject pinch_object_position = new GameObject("pinch_object_position");
                pinch_object_position.transform.position = centor_pos;
                thumb_script.collision_object.transform.parent = pinch_object_position.transform;
            }else{
                // 物体の位置を表すGameobjectの位置を2点間の中心へ移動
                obj.transform.position = centor_pos;
            } 

        }else{
            // pinch_object_positionが存在する場合，そこからオブジェクトを取り出しpinch_object_positionは消しておく
            GameObject obj=GameObject.Find("pinch_object_position");
            if(!(obj==null)){
                Debug.Log("detach children");
                obj.transform.transform.DetachChildren();
                Destroy(obj);
            }
        }

    }
}   
