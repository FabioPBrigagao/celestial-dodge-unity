using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFeatures : MonoBehaviour{


    public int health;
    public ParticleSystem deathParticles;
    public ParticleSystem hitParticles;
    public int bonusScore;
    public float speed;

    public void Damage(){
        health -= 1;
        StartCoroutine(CameraController.current.ShakeCamera(0.1f, 1.5f));
        FlashWhite();
        if(health <= 0){
            Die();
        }
    }

    //Enemy flash when hit
    public void FlashWhite(){
        GetComponent<SpriteRenderer>().material = GameAssets.i.flash;
        Invoke("ResetMaterial", 0.1f);
        if(hitParticles != null){
            Instantiate(hitParticles, transform.position, transform.rotation, gameObject.transform);
        }
    }

    public void ResetMaterial(){
        GetComponent<SpriteRenderer>().material = GameAssets.i.defaultSprite;
    }

    void Die(){
        Instantiate(deathParticles, transform.position, transform.rotation);
        speed = 0;
        Destroy(gameObject, 0.3f);
        BonusScoreDisplay();
    }

    void BonusScoreDisplay(){
        BonusScore.Create(transform.position, bonusScore);
        SceneManagerEndless.current.AddBonusScore(bonusScore);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Bullet"){
            Damage();
            collision.gameObject.SetActive(false);
        }
    }



}
