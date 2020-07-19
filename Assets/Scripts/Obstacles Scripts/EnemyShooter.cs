using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour, IDefeatable {

    public int health;
    public int bonus;
    public float fadeOutMultiplier;
    public ParticleSystem deathParticles;
    public PolygonCollider2D col;
    public Transform firePos;
    public SpriteRenderer sprite;

    private Vector3 currentTargetPos;
    private bool defeated = false;
    private bool arrivedPos;
    private float countWaitTime;
    private float countTimeBtwShoot;

    private const float X_MIN_SPAWN_POS = -6;
    private const float X_MAX_SPAWN_POS = 6;
    private const float Y_MIN_SPAWN_POS = -10;
    private const float Y_MAX_SPAWN_POS = 10;

    public static Transform Create(Vector2 position) {
        return Instantiate(GameAssets.i.enemyShooter, position, Quaternion.identity);
    }

    void Awake() {
        currentTargetPos = UtilitiesMethods.XYRandomVector3(X_MIN_SPAWN_POS, X_MAX_SPAWN_POS, Y_MIN_SPAWN_POS, Y_MAX_SPAWN_POS, transform.position.z);
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
            BulletController.Fire(firePos, false);
            countTimeBtwShoot = DifficultyController.instance.shooterTimeBtwShoots;
        } else countTimeBtwShoot -= Time.deltaTime;
    }

    IEnumerator FadeOut() {
        for (float i = 1f; i >= 0; i -= Time.deltaTime * fadeOutMultiplier) {
            Color color = sprite.material.color;
            color.a = i;
            sprite.material.color = color;
            yield return null;
        }
        Destroy(gameObject);
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
        ParticleSystem obj_DeathParticles = Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(obj_DeathParticles, 5f);
        StartCoroutine(FadeOut());
        BonusScoreDisplay();
    }

    public IEnumerator FlashWhite() {
        sprite.material = GameAssets.i.flash;
        yield return new WaitForSeconds(0.1f);
        sprite.material = GameAssets.i.defaultSprite;
    }

    public void BonusScoreDisplay() {
        BonusScore.Create(transform.position, bonus);
        ScoreController.instance.bonus = bonus;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Damage();
            collision.gameObject.SetActive(false);
        }
    }
}
