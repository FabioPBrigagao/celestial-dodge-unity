using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour{

    public static ObjectPooling current;
    public GameObject PooledObj;
    public int pooledAmount;
    public bool willGrow;

    List<GameObject> poolOfObj;

    void Awake() => current = this;

    public void Start(){
        poolOfObj = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++){
            GameObject obj = (GameObject)Instantiate(PooledObj,gameObject.transform);
            obj.SetActive(false);
            poolOfObj.Add(obj);
        }
    }

    public GameObject GetObjFromPool(){
        for (int i = 0; i < poolOfObj.Count; i++){
            if(!poolOfObj[i].activeInHierarchy){
                return poolOfObj[i];
            }
        }
        if(willGrow){
            GameObject obj = (GameObject)Instantiate(PooledObj, gameObject.transform);
            poolOfObj.Add(obj);
            return obj;
        }
        return null;
    }
}
