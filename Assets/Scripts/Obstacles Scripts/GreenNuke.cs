using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenNuke : MonoBehaviour{

    public float speed;
    public int health;
    public float timer;
    public Animator animator;

    private float timerCount;
    private float aniSpeed = 1;
    private GameObject player;
    CameraController cameraClass;
    SceneManagerEndless managerClass;

    private const int BONUS_SCORE = 20;

    public static void Create(Transform transform){
        Instantiate(GameAssets.i.greenNuke, transform.position, transform.rotation);
    }

    void Awake(){
        player = SceneManagerEndless.current.player;
    }

    void Start(){
        Invoke("Destruction", timer);
    }

    void Update(){
        FollowPlayer();
        Flashing();  
    }

    void FollowPlayer(){
        if (player != null){
            Vector2 direction = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            transform.up = direction;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
    }

    void Flashing(){
        aniSpeed += (Time.deltaTime / 1000)*2;
        animator.SetFloat("Multiplier", aniSpeed);
    }

    void Damage(){
        health -= 1;
        StartCoroutine(CameraController.current.ShakeCamera(0.1f, 1.5f));
        Instantiate(GameAssets.i.nukeHitParticles, transform.position, transform.rotation,gameObject.transform); //death particle appear in the moment of collision
        if (health == 0){
           Destruction();
           BonusScoreDisplay();
        }
    }

    void Destruction(){
        Explosion.Create(transform.position);
        StartCoroutine(CameraController.current.ShakeCamera(0.5f, 2f));
        Destroy(gameObject);
    }

    void BonusScoreDisplay(){
        BonusScore.Create(transform.position, BONUS_SCORE);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Bullet"){
            Damage();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Player"){
            Destruction();
        }

    }
}
