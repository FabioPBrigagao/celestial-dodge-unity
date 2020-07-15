using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour{


    public float speed;
    public float lifeTime;

    void OnEnable(){
        Invoke("Destroy", lifeTime);
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void Destroy() => gameObject.SetActive(false);

    void OnDisable(){
        CancelInvoke();
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 0;
    }

    public static void Fire(Transform transPos, bool ifplayerShooting){

        GameObject ObjPol;

        if(ifplayerShooting){
            ObjPol = GameObject.Find("ObjPool_PlayerBullet");
        }else{
            ObjPol = GameObject.Find("ObjPool_EnemyBullet");
        }

        GameObject Obj = ObjPol.GetComponent<ObjectPooling>().GetObjFromPool();
        Obj.transform.position = transPos.position;
        if(ifplayerShooting) Obj.transform.rotation = transPos.transform.rotation;
        else Obj.transform.rotation = Obj.transform.rotation;
        Obj.SetActive(true);
    }




}
