using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour{

    public float speed;
    public float lifeTime;

    private float score;
    private float startSpeed;
    private int randomBinaryNum;

    private const float MAX_SPEED = 35; 
    private const float ROTATION_RATE = 0.3f;

    public static void Create(int asteroidIndex, Vector2 position){
        Instantiate(GameAssets.i.asteroidArray[asteroidIndex], position, Quaternion.identity);
    }

    void Start(){
        startSpeed = speed;
        randomBinaryNum = Random.Range(0, 2);
    }

    void Update(){
        IncreaseSpeed();
        Rotation();
        Destroy();
    }

    //As score increases, speed of asteroid also increases
    //Asteroid speed increase selection is random
    void IncreaseSpeed(){ 
        if(randomBinaryNum == 0 && speed < MAX_SPEED){
            score = SceneManagerEndless.current.GetScore(); 
            speed = startSpeed + (score / 10); 
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -1 * speed);
    }

    void Rotation(){
        gameObject.GetComponent<Rigidbody2D>().rotation = gameObject.GetComponent<Rigidbody2D>().rotation + ROTATION_RATE; 
    }


    void Destroy(){
        if(gameObject.transform.position.y < SceneManagerEndless.current.destroyPos[0].position.y){
            Destroy(gameObject);
        }
    }
}



