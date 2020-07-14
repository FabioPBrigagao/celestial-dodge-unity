using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour{

    public float speed;

    private Rigidbody2D rb;
    private float startspeed;
    private float score;

    SceneManagerEndless managerClass;

    public static void Create(Vector2 position){
        WarningSign.Create(position);
        if (position.x < 0){
            Instantiate(GameAssets.i.missile, position, Quaternion.identity);
        }else{
            Instantiate(GameAssets.i.missile, position, Quaternion.Euler(0, 180, 0));
        } 
    }

     //Retrieve Object from ObjectPooling
    public static void GetFromPool(Vector2 position){
        GameObject ObjPol;
        ObjPol = GameObject.Find("ObjPool_Missile");
        GameObject Obj = ObjPol.GetComponent<ObjectPooling>().GetObjFromPool();
        WarningSign.GetFromPool(position);
        Obj.transform.position = position;
        if (position.x < 0) Obj.transform.rotation = Quaternion.identity;
        else Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
        Obj.SetActive(true);
    }

    void Start(){
        startspeed = speed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update(){
        score = SceneManagerEndless.current.score;
        speed = startspeed + (score / 100);
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Destroy_Object"){
            gameObject.SetActive(false);
        }
    }


}
