using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float timeBtwShoot;
    public Transform firePos; 
    public GameObject flyingParticles;
    public GameObject deathParticles;
    
    public LayerMask obstacleLayer;
    [HideInInspector] public bool startPos = false;

    [Header("Cache Components")]
    public Animator ani;
    public PolygonCollider2D col;
    public Camera cam;

    private Touch touch;
    private Vector2 touchPosition;
    private Vector2 mousePosition;
    private ParticleSystem.EmissionModule emission;

    private float deltaX;
    private float deltaY;
    private float countTimeBtwShoot;
    private float init_flyingParticleEmission;

    private const float VERTICAL_BOUNDARIES = 15f;
    private const float HORIZONTAL_BOUNDARIES = 8f;
    private const float FLYING_PARTICLE_EMISSION_BOOST = 400f;
    private const int HASH_CODE_STARTGAME_ANIMATOR = -1038509842;

    void Awake() {
        emission = flyingParticles.GetComponent<ParticleSystem>().emission;
        init_flyingParticleEmission = emission.rateOverTime.constant;
    }

    void FixedUpdate() {
        if (startPos == true) {
            if (SystemInfo.deviceType == DeviceType.Desktop) Mouse();
            if (SystemInfo.deviceType == DeviceType.Handheld) Touch();
        }
    }

    void StartPosition() {
        ani.SetTrigger(HASH_CODE_STARTGAME_ANIMATOR);
        ani.applyRootMotion = true;
        startPos = true;
    }

    void Mouse() {
        if (Input.GetMouseButton(0)) {
            Shoot();
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) {
                deltaX = mousePosition.x - transform.position.x;
                deltaY = mousePosition.y - transform.position.y;

            }
            if ((mousePosition.x - deltaX) < HORIZONTAL_BOUNDARIES &&
                                                   (mousePosition.x - deltaX) > -HORIZONTAL_BOUNDARIES &&
                                                   (mousePosition.y - deltaY) > -VERTICAL_BOUNDARIES &&
                                                   (mousePosition.y - deltaY) < VERTICAL_BOUNDARIES) {
                transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
            }
            emission.rateOverTime = FLYING_PARTICLE_EMISSION_BOOST;
        }
        if (Input.GetMouseButtonUp(0)) {
            emission.rateOverTime = init_flyingParticleEmission;
        }
    }

    void Touch() {
        if (Input.touchCount > 0) {
            Shoot();
            touch = Input.GetTouch(0);
            touchPosition = cam.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began) {
                deltaX = touchPosition.x - transform.position.x;
                deltaY = touchPosition.y - transform.position.y;
            }
            if (touch.phase == TouchPhase.Moved && (touchPosition.x - deltaX) < HORIZONTAL_BOUNDARIES &&
                                                   (touchPosition.x - deltaX) > -HORIZONTAL_BOUNDARIES &&
                                                   (touchPosition.y - deltaY) > -VERTICAL_BOUNDARIES &&
                                                   (touchPosition.y - deltaY) < VERTICAL_BOUNDARIES) {
                transform.position = new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY);
                emission.rateOverTime = FLYING_PARTICLE_EMISSION_BOOST;
            }
            if (touch.phase == TouchPhase.Ended) {
                emission.rateOverTime = init_flyingParticleEmission;
            }
        }
    }

    void Shoot() {
        if (WaveController.instance.waveNumber == 2 || WaveController.instance.waveNumber == 4) {
            if (countTimeBtwShoot <= 0) {
                BulletController.Fire(firePos, WaveController.instance.poolPlayerBullet);
                countTimeBtwShoot = timeBtwShoot;
            }
            countTimeBtwShoot -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(obstacleLayer)) {
            Destroy(this.gameObject);
            GameObject objDeathParticles = Instantiate(deathParticles, transform.position, transform.rotation);
            Destroy(objDeathParticles, 8);
        }
    }
}
