using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyShooter : MonoBehaviour{

    public Transform firePos;
    public float waitTime;
    public float timeBtwShoot;

    private float speed;
    private int tempXPos;
    private Vector2[,] enemyPos;
    private int enemyPosIndex;
    private bool arrivedPos = false;
    private float countWaitTime;
    private float startSpeed;
    private float startWaitTime;

    private const float MIN_TIME_BTW_SHOOT = 0.4f;
    private const float MAX_SPEED = 20;
    private const float MIN_WAIT_TIME = 1;

    public static void Create(Vector2 position){
        Instantiate(GameAssets.i.greenEnemyShooter, position, Quaternion.identity);
    }

    void Awake(){
        enemyPos = new Vector2[3, 3];

        //Position A                                      Position B                                 Position C                                        
        enemyPos[0,0] = new Vector2(-6, 8);    enemyPos[1, 0] = new Vector2(-6, 2);    enemyPos[2, 0] = new Vector2(-6, -4);
        enemyPos[0,1] = new Vector2(0, 8);     enemyPos[1, 1] = new Vector2(0, 2);     enemyPos[2, 1] = new Vector2(0, -4);
        enemyPos[0,2] = new Vector2(6, 8);     enemyPos[1, 2] = new Vector2(6, 2);     enemyPos[2, 2] = new Vector2(6, -4);
    }

    void Start(){
        InvokeRepeating("Shoot",1,timeBtwShoot);
        startSpeed = GetComponent<EnemyFeatures>().speed;
        speed = startSpeed;
        startWaitTime = waitTime;
        tempXPos = Random.Range(0, 3);
        enemyPosIndex = Random.Range(0, 3);
    }

    void Update(){
        IncreaseSpeedAndShootRate();
        EnemyMovement();
    }

    // Enemy moves between positions
    void EnemyMovement(){
        if (arrivedPos == false && countWaitTime <= 0){
            transform.position = Vector2.MoveTowards(transform.position, enemyPos[enemyPosIndex, tempXPos], speed * Time.deltaTime); 
            if ((Vector2)transform.position == enemyPos[enemyPosIndex, tempXPos]){
                arrivedPos = true;
                countWaitTime = waitTime;
            }
        }else if(countWaitTime > 0){
            countWaitTime -= Time.deltaTime;
        }else{
            enemyPosIndex = Random.Range(0, 3);
            tempXPos = Random.Range(0, 3);
            arrivedPos = false;
        }
        //If speed is 0, enemy has been defeated
        if (GetComponent<EnemyFeatures>().speed == 0) speed = 0;
    }

    //As score increases, enemy speed and shoot rate also increases
    void IncreaseSpeedAndShootRate(){
        float score = SceneManagerEndless.current.GetScore();
        if (waitTime > MIN_WAIT_TIME) waitTime = startWaitTime - (score / 1000);
        if (speed < MAX_SPEED && speed != 0) speed = startSpeed + (score / 100);
    }

    void Shoot(){
        BulletController.Fire(firePos, false);
    }
}
