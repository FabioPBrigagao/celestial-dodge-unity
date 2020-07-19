﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour, IDefeatable {

    public int health;
    public int bonus;
    [HideInInspector] public float timer;
    public SpriteRenderer sprite;
    public ParticleSystem hitParticles;

    private GameObject player;
    private const float FLASH_TIME_RATE = 20;

    public static GuidedMissile Create(Vector2 position) {
        Transform transGuidedMissile = Instantiate(GameAssets.i.guidedMissile, position, Quaternion.identity);
        GuidedMissile guidedMissile = transGuidedMissile.GetComponent<GuidedMissile>();
        return guidedMissile;
    }

    void Awake() {
        player = WaveController.instance.player;
        StartCoroutine(FlashWhite());
        timer = DifficultyController.instance.guidedMissileTimer;
    }

    void Update() {
        FollowPlayer();
        Destruction();
    }

    void FollowPlayer() {
        if (player != null) {
            Vector2 direction = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            transform.up = direction;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, DifficultyController.instance.guidedMissileSpeed * Time.deltaTime);
        }
    }

    public void Damage() {
        health -= 1;
        StartCoroutine(CameraController.current.ShakeCamera(0.1f, 1.5f));
        ParticleSystem ObjHitParticles = Instantiate(hitParticles, transform.position, transform.rotation, gameObject.transform);
        Destroy(ObjHitParticles, 4);
        if (health <= 0) {
            timer = 0;
            BonusScoreDisplay();
        }
    }

    public void Destruction() {
        if (timer <= 0 || player == null) {
            Explosion.Create(transform.position);
            StartCoroutine(CameraController.current.ShakeCamera(0.5f, 2f));
            Destroy(gameObject);
        } else timer -= Time.deltaTime;
    }

    public void BonusScoreDisplay() {
        BonusScore.Create(transform.position, bonus);
    }

    public IEnumerator FlashWhite() {
        while(timer >= 0 ){
            sprite.material = GameAssets.i.flash;
            yield return new WaitForSeconds(timer/ FLASH_TIME_RATE);
            sprite.material = GameAssets.i.defaultSprite;
            yield return new WaitForSeconds(timer/ FLASH_TIME_RATE);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Damage();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Player") {
            timer = 0;
        }
    }
}
