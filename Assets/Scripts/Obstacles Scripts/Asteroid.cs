using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour{

    public float speed;
    private int randomBinaryNum;
    private const float ROTATION_RATE = 0.3f;

    public static void Create(int asteroidIndex, Vector2 position){
        Instantiate(GameAssets.i.asteroidArray[asteroidIndex], position, Quaternion.identity);
    }

    void Start(){
        SetVelocity();
    }

    void Update(){
        Rotation();
        Destroy();
    }

    void SetVelocity(){
        randomBinaryNum = Random.Range(0, 2);
        if (randomBinaryNum == 0){
            speed = DifficultyController.instance.asteroidSpeed;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -1 * speed);
    }

    void Rotation(){
        gameObject.GetComponent<Rigidbody2D>().rotation = gameObject.GetComponent<Rigidbody2D>().rotation + ROTATION_RATE; 
    }

    void Destroy(){
        if(gameObject.transform.position.y < WaveController.instance.destroyPositionsArray[0].position.y){
            Destroy(gameObject);
        }
    }
}



