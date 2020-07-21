using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public static WaveController instance;

    public float waveDuration;
    [HideInInspector] public int waveNumber;
    public GameObject player;

    [Header("Object Pooling")]
    public ObjectPooling poolMissile;
    public ObjectPooling poolEnemyBullet;
    public ObjectPooling poolPlayerBullet;

    [Header("Position Array")]
    public Transform[] spawn;
    public Transform[] destroy;

    private int previousWave;
    private int randomAsteroid;
    private float countWaveDuration;
    private float spawnTimer;
    private bool aimPlayer;
    private int obstacleCount;
    private List<GameObject> obstaclesList = new List<GameObject>();
    private Vector2 spawnPosition = new Vector2(0, 0);
    private List<int> waveCount = new List<int>();

    const float ABSOLUTE_X_SPAWN = 7;
    const float ABSOLUTE_Y_SPAWN = 13;
    const int TOTAL_NUMBER_OF_WAVES = 4;

    void Awake() => instance = this;

    void Start() {
        WaveManager();
    }

    void Update() {
        if (player != null && player.GetComponent<Player>().startPos == true) {
            switch (waveNumber) {
                case 1:
                    Asteroids();
                    break;
                case 2:
                    EnemyShooters();
                    break;
                case 3:
                    Missiles();
                    break;
                case 4:
                    GuidedMissiles();
                    break;
                default:
                    break;
            }
            if(countWaveDuration > 0) countWaveDuration -= Time.deltaTime;
        }
    }

    void WaveManager() {
        obstacleCount = UtilitiesMethods.CheckEnableList(obstaclesList);
        if (obstacleCount == 0) {
            obstaclesList.Clear();
            spawnTimer = 0;
            if (waveCount.Count == TOTAL_NUMBER_OF_WAVES) {
                waveCount.Clear();
            } else {
                previousWave = waveNumber;
                waveNumber = UtilitiesMethods.RandomIntegerException(1, (TOTAL_NUMBER_OF_WAVES + 1), previousWave, waveCount);
                countWaveDuration = waveDuration;
                waveCount.Add(waveNumber);
            }
        } 
    }

    void Asteroids() {
        randomAsteroid = Random.Range(0, GameAssets.i.asteroidArray.Length);
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                spawnPosition.x = Random.Range(-ABSOLUTE_X_SPAWN, ABSOLUTE_X_SPAWN);
                spawnPosition.y = spawn[0].position.y;
                GameObject obj_Asteroid = Asteroid.Create(randomAsteroid, spawnPosition);
                obstaclesList.Add(obj_Asteroid);
                spawnTimer = DifficultyController.instance.asteroidSpawnRate;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }

    void EnemyShooters() {
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                spawnPosition.x = Random.Range(-ABSOLUTE_X_SPAWN, ABSOLUTE_X_SPAWN);
                spawnPosition.y = spawn[0].position.y;
                GameObject obj_Shooter = Shooter.Create(spawnPosition);
                obstaclesList.Add(obj_Shooter);
                spawnTimer = DifficultyController.instance.shooterSpawnRate;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }

    void Missiles() {
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                float tempPosY = 0;
                int tempPosIndex = Random.Range(1, 3);
                if (aimPlayer == false) {
                    tempPosY = Random.Range(-ABSOLUTE_Y_SPAWN, ABSOLUTE_Y_SPAWN);
                    aimPlayer = true;
                } else if (player != null) {
                    tempPosY = player.transform.position.y;
                    aimPlayer = false;
                }
                spawnPosition.x = spawn[tempPosIndex].position.x;
                spawnPosition.y = tempPosY;
                GameObject obj_Missile = Missile.GetFromPool(spawnPosition, poolMissile);
                obstaclesList.Add(obj_Missile);
                spawnTimer = DifficultyController.instance.missileSpawnRate;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }

    void GuidedMissiles() {
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                int tempPosIndex = Random.Range(0, 3);
                if (tempPosIndex == 0) {
                    spawnPosition.y = spawn[tempPosIndex].position.y;
                    spawnPosition.x = Random.Range(-ABSOLUTE_X_SPAWN, ABSOLUTE_X_SPAWN);
                } else {
                    spawnPosition.y = Random.Range(-ABSOLUTE_Y_SPAWN, ABSOLUTE_Y_SPAWN);
                    spawnPosition.x = spawn[tempPosIndex].position.x;
                }
                GameObject Obj_guidedMissile = GuidedMissile.Create(spawnPosition);
                obstaclesList.Add(Obj_guidedMissile);
                spawnTimer = DifficultyController.instance.guidedMissileTimer;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }
}
