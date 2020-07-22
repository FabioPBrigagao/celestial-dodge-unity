using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour{

    public float speed;
    public Transform frontPosition;
    public Transform backPosition;

    private float respawnPos;
    private float resetPos;

    void Awake(){
        DontDestroyOnLoad(gameObject);
        respawnPos = frontPosition.position.y;
        resetPos = backPosition.position.y;
    }

    void Update(){
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - speed * Time.deltaTime);
        if (gameObject.transform.position.y <= resetPos){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, respawnPos);
        }
    }
}
