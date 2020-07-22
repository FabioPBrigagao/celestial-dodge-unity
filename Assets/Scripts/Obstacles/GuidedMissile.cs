using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour, IDefeatable {

    public int bonus;
    [HideInInspector] public float timer;
    public ParticleSystem hitParticles;
    public LayerMask playerLayer;
    public LayerMask bulletLayer;

    [Header("Cache Components")]
    public SpriteRenderer spr;
    public PolygonCollider2D col;

    private GameObject player;
    private int health;
    private const float FLASH_TIME_RATE = 20;

    public static GameObject GetFromPool(Vector2 position, ObjectPooling pool) {
        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = position;
        Obj.SetActive(true);
        return Obj;
    }

    void OnEnable() {
        StartCoroutine(FlashWhite());
        timer = DifficultyController.instance.guidedMissileTimer;
        health = DifficultyController.instance.guidedMissileHealth;
    }


    void Awake() {
        player = WaveController.instance.playerScript.gameObject;
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
        hitParticles.Play();
        if (health <= 0) {
            timer = 0;
            BonusScoreDisplay();
        }
    }

    public void Destruction() {
        if (timer <= 0 || player == null) {
            Explosion.GetFromPool(transform.position, WaveController.instance.poolExplosion);
            StartCoroutine(CameraController.current.ShakeCamera(0.5f, 2f));
            gameObject.SetActive(false);
        } else timer -= Time.deltaTime;
    }

    public void BonusScoreDisplay() {
        BonusScore.Create(transform.position, bonus);
    }

    public IEnumerator FlashWhite() {
        while(timer >= 0 ){
            spr.material = GameAssets.i.flash;
            yield return new WaitForSeconds(timer/ FLASH_TIME_RATE);
            spr.material = GameAssets.i.defaultSprite;
            yield return new WaitForSeconds(timer/ FLASH_TIME_RATE);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(bulletLayer)) {
            Damage();
            collision.gameObject.SetActive(false);
        }
        if (col.IsTouchingLayers(playerLayer)) {
            timer = 0;
        }
    }
}
