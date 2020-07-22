using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IDefeatable {

    public int bonus;
    public float fadeOutMultiplier;
    public ParticleSystem deathParticles;
    public Transform firePos;
    public LayerMask bulletLayer;

    [Header("Cache Components")]
    public SpriteRenderer spr;
    public PolygonCollider2D col;

    private Vector3 currentTargetPos;
    private bool defeated;
    private int init_health;
    private bool arrivedPos;
    private float countWaitTime;
    private float countTimeBtwShoot;
    private int health;

    private const float X_MIN_SPAWN_POS = -6;
    private const float X_MAX_SPAWN_POS = 6;
    private const float Y_MIN_SPAWN_POS = -10;
    private const float Y_MAX_SPAWN_POS = 10;

    public static GameObject GetFromPool(Vector2 position, ObjectPooling pool) {
        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = position;
        Obj.SetActive(true);
        return Obj;
    }

    void OnEnable() {
        currentTargetPos = UtilitiesMethods.XYRandomVector3(X_MIN_SPAWN_POS, X_MAX_SPAWN_POS, Y_MIN_SPAWN_POS, Y_MAX_SPAWN_POS, transform.position.z);
        spr.material.color = GameAssets.i.defaultSprite.color;
        col.enabled = true;
        countTimeBtwShoot = DifficultyController.instance.shooterTimeBtwShoots;
        health = DifficultyController.instance.shooterHealth;
        defeated = false;
    }

    void Update() {
        EnemyMovement();
        Shoot();
    }

    void EnemyMovement() {
        if (!defeated) {
            if (arrivedPos == false && countWaitTime <= 0) {
                transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, DifficultyController.instance.shooterSpeed * Time.deltaTime);
                if (transform.position == currentTargetPos) {
                    arrivedPos = true;
                    countWaitTime = DifficultyController.instance.shooterWaitTime;
                }
            } else if (countWaitTime > 0) {
                countWaitTime -= Time.deltaTime;
            } else {
                currentTargetPos = UtilitiesMethods.XYRandomVector3(X_MIN_SPAWN_POS, X_MAX_SPAWN_POS, Y_MIN_SPAWN_POS, Y_MAX_SPAWN_POS, transform.position.z);
                arrivedPos = false;
            }
        }
    }

    void Shoot() {
        if (countTimeBtwShoot < 0 && !defeated) {
            BulletController.Fire(firePos, WaveController.instance.poolEnemyBullet);
            countTimeBtwShoot = DifficultyController.instance.shooterTimeBtwShoots;
        } else countTimeBtwShoot -= Time.deltaTime;
    }

    IEnumerator FadeOut() {
        for (float i = 1f; i >= 0; i -= Time.deltaTime * fadeOutMultiplier) {
            Color color = spr.material.color;
            color.a = i;
            spr.material.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void Damage() {
        health -= 1;
        StartCoroutine(CameraController.current.ShakeCamera(0.1f, 1.5f));
        StartCoroutine(FlashWhite());
        if (health <= 0 && !defeated) {
            Destruction();
            defeated = true;
        }
    }

    public void Destruction() {
        col.enabled = false;
        deathParticles.Play();
        StartCoroutine(FadeOut());
        BonusScoreDisplay();
    }

    public IEnumerator FlashWhite() {
        spr.material = GameAssets.i.flash;
        yield return new WaitForSeconds(0.1f);
        spr.material = GameAssets.i.defaultSprite;
    }

    public void BonusScoreDisplay() {
        BonusScore.Create(transform.position, bonus);
        ScoreController.instance.bonus = bonus;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(bulletLayer)) {
            Damage();
            collision.gameObject.SetActive(false);
        }
    }
}
