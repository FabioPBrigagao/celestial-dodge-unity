using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public static WaveController instance;

    public float waveDuration;
    public GameObject player;
    public ObjectPooling missilePool;
    public Transform[] spawnPositionsArray;
    public Transform[] destroyPositionsArray;

    [HideInInspector] public int waveNumber;
    private int previousWave;
    private int randomAsteroid;
    private float countWaveDuration;
    private float spawnTimer;
    private bool aimPlayer;
    private GameObject[] obstaclesArray;
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
                    previousWave = 1;
                    break;
                case 2:
                    EnemyShooters();
                    previousWave = 2;
                    break;
                case 3:
                    Missiles();
                    previousWave = 3;
                    break;
                case 4:
                    GuidedMissiles();
                    previousWave = 4;
                    break;
                default:
                    break;
            }
            countWaveDuration -= Time.deltaTime;
        }
    }

    void WaveManager() {
        obstaclesArray = GameObject.FindGameObjectsWithTag("Obstacle");
        if (obstaclesArray.Length == 0) {
            spawnTimer = 0;
            if (waveCount.Count == TOTAL_NUMBER_OF_WAVES) {
                waveCount.Clear();
            } else {
                waveNumber = UtilitiesMethods.RandomIntegerException(1, (TOTAL_NUMBER_OF_WAVES + 1), previousWave, waveCount);
                countWaveDuration = waveDuration;
                waveCount.Add(waveNumber);
            }
        } else {
            countWaveDuration -= Time.deltaTime;
        }
    }

    void Asteroids() {
        randomAsteroid = Random.Range(0, GameAssets.i.asteroidArray.Length);
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                spawnPosition.x = Random.Range(-ABSOLUTE_X_SPAWN, ABSOLUTE_X_SPAWN);
                spawnPosition.y = spawnPositionsArray[0].position.y;
                Asteroid.Create(randomAsteroid, spawnPosition);
                spawnTimer = DifficultyController.instance.asteroidSpawnRate;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }

    void EnemyShooters() {
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                spawnPosition.x = Random.Range(-ABSOLUTE_X_SPAWN, ABSOLUTE_X_SPAWN);
                spawnPosition.y = spawnPositionsArray[0].position.y;
                EnemyShooter.Create(spawnPosition);
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
                spawnPosition.x = spawnPositionsArray[tempPosIndex].position.x;
                spawnPosition.y = tempPosY;
                Missile.GetFromPool(spawnPosition, missilePool);
                spawnTimer = DifficultyController.instance.missileSpawnRate;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }

    void GuidedMissiles() {
        if (countWaveDuration > 0) {
            if (spawnTimer <= 0) {
                int tempPosIndex = Random.Range(0, 3);
                if (tempPosIndex == 0) {
                    spawnPosition.y = spawnPositionsArray[tempPosIndex].position.y;
                    spawnPosition.x = Random.Range(-ABSOLUTE_X_SPAWN, ABSOLUTE_X_SPAWN);
                } else {
                    spawnPosition.y = Random.Range(-ABSOLUTE_Y_SPAWN, ABSOLUTE_Y_SPAWN);
                    spawnPosition.x = spawnPositionsArray[tempPosIndex].position.x;
                }
                GuidedMissile guidedMissile = GuidedMissile.Create(spawnPosition);
                spawnTimer = guidedMissile.timer;
            } else spawnTimer -= Time.deltaTime;
        } else WaveManager();
    }
}
