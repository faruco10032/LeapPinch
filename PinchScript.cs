using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchScript : MonoBehaviour
{
    public GameObject thumb;
    public GameObject index;
    public GameObject rotation_ref_obj;
    CollisionScript thumb_script;
    CollisionScript index_script;

    // public GameObject collision_object;

    // つまんだ瞬   間の指の回転量
    Vector3 first_rot;

    public bool pinch_flag = false;

    // Start is called before the first frame update
    void Start(){
        //Gameobjectのスクリプトコンポーネントを取得
        thumb_script = thumb.gameObject.GetComponent<CollisionScript>();
        index_script = index.gameObject.GetComponent<CollisionScript>();
        Debug.Log("Start!");
    }

    void Update(){
        //指の球体コライダーの位置をベクトルで取得
        Vector3 thumb_pos = thumb.transform.position;
        Vector3 index_pos = index.transform.position;
        // 指の距離を取得
        float finger_distance = Vector3.Distance(thumb_pos, index_pos);

        GameObject pinch_object;

        // 球の接触判定を取得．つまんでる間実行する．
        if(thumb_script.collision_flag&&index_script.collision_flag&&
        (finger_distance<(index_script.collider_radius+thumb_script.collider_radius+2.0*thumb_script.R))&&
        (thumb_script.collision_object.gameObject.tag == "isEnablePinch")){//親指と人差し指両方で触れており，ふたつの距離が接触物体の直径以下でつまみ可能タグを持っているの場合
            // つまみオブジェクトを更新
            pinch_object = thumb_script.collision_object;
            pinch_flag = true;
            // Debug.Log("pinch now");
            //2点間の中心を計算
            Vector3 centor_pos = (thumb_pos+index_pos)*0.5f;

            // 座標を示す空のGameobjectがなければ2点間の中心座標に空のGameobjectを作り，親指と触れている物体をその子にする
            GameObject obj = GameObject.Find("pinch_object_position");
            if(obj==null){
                GameObject pinch_object_position = new GameObject("pinch_object_position");
                pinch_object_position.transform.position = centor_pos;
                thumb_script.collision_object.transform.parent = pinch_object_position.transform;

                // つまんだ瞬間の角度を取得
                first_rot = rotation_ref_obj.transform.eulerAngles;
                Debug.Log("OnPinch");
            }

            // 物体のつまみ位置を表すGameobjectの位置を2点間の中心へ移動
            obj.transform.position = centor_pos;

            // つまんだ瞬間からの指の回転量を計算
            Vector3 diff_rot = -first_rot + rotation_ref_obj.transform.eulerAngles;
            // 物体のつまみ位置を表すGameobjectの回転を指と同期させる
            obj.transform.localEulerAngles = diff_rot;

            // つまみ物体のGravityが有効だったら，つまんでる物体のGravityを切る
            // つまんでいる物体のRigidbodyを取得
            Rigidbody rb = pinch_object.GetComponent<Rigidbody>();
            rb.useGravity = false;
            // Debug.Log("diff_rot : "+diff_rot);
            // Debug.Log("obj_rot  : "+obj.transform.localEulerAngles);

            // つまんでる間は慣性力を受けないようにする
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;


        }else{ // つまんでいないとき
            // 離すときに元の状態に戻す処理
            if(pinch_flag==true){
                // pinch_object_positionが存在する場合，そこからオブジェクトを取り出しpinch_object_positionは消しておく
                GameObject obj=GameObject.Find("pinch_object_position");

                // // オブジェクト（つまみ物体）にGravityを入れておく
                // // Rigidbody rb = obj.transform.FindChild("Child").gameObject.GetComponent<Rigidbody>();
                // Debug.Log("Gravity false");
                // Debug.Log("pinch flag : "+pinch_flag);
                
                // Rigidbody _rb = _collision_object.GetComponent<Rigidbody>();
                // _rb.useGravity=true;

                // Debug.Log("detach children");
                // 子から解除しておく
                obj.transform.transform.DetachChildren();
                Destroy(obj);

                pinch_object = null;
                pinch_flag = false;
            }else{
                // Debug.Log("dont pinch");
            }

        }

    }
}   
