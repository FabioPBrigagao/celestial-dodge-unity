using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour{

    private const float ABS_MAX_X_TOP_SPAWN = 7;

    public static void Create(Vector2 position){
        float side = 1;
        if (position.x < 0){
            side = -1;
        }
        Vector2 ObjPosition = new Vector2((ABS_MAX_X_TOP_SPAWN * side), position.y);
        Instantiate(GameAssets.i.warningSign, ObjPosition, Quaternion.identity);
    }

    //Retrieve Object from ObjectPooling
    public static void GetFromPool(Vector2 position){
        GameObject ObjPol;
        ObjPol = GameObject.Find("ObjPool_WarningSign");
        GameObject Obj = ObjPol.GetComponent<ObjectPooling>().GetObjFromPool();
        float side = 1;
        if (position.x < 0) side = -1;
        Vector2 ObjPosition = new Vector2((ABS_MAX_X_TOP_SPAWN * side), position.y);
        Obj.transform.position = ObjPosition;
        Obj.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Obstacle"){
            gameObject.SetActive(false);
        }
    }
}
