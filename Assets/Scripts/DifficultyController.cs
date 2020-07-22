using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DifficultyController : MonoBehaviour {

    public static DifficultyController instance;

    private GameObject player;
    
    public float asteroidSpawnRate, init_asteroidSpawnRate, limit_asteroidSpawnRate;
    public float asteroidSpeed, init_asteroidSpeed, limit_asteroidSpeed;

    public float shooterSpawnRate, init_ShooterSpawnRate, limit_ShooterSpawnRate;
    public float shooterSpeed, init_ShooterSpeed, limit_ShooterSpeed;
    public float shooterWaitTime, init_ShooterWaitTime, limit_ShooterWaitTime;
    public float shooterTimeBtwShoots, init_ShooterTimeBtwShoots, limit_ShooterTimeBtwShoots;
    public int shooterHealth;

    public float missileSpawnRate, init_MissileSpawnRate, limit_MissileSpawnRate;
    public float missileSpeed, init_MissileSpeed, limit_MissileSpeed;

    public float guidedMissileTimer, init_GuidedMissileTimer, limit_GuidedMissileTimer;
    public float guidedMissileSpeed, init_GuidedMissileSpeed, limit_GuidedMissileSpeed;
    public int guidedMissileHealth;

    void Awake() => instance = this;

    void Start() {
        player = WaveController.instance.player;
    }

    void Update() {
        if (player != null) Difficulty();
    }

    void Difficulty(){
        float diffRate = ScoreController.instance.difficultyRate;
        switch (WaveController.instance.waveNumber) {
            case 1:
                asteroidSpawnRate = UtilitiesMethods.CalculateDifficultyMetric(init_asteroidSpawnRate, limit_asteroidSpawnRate, diffRate);
                asteroidSpeed = UtilitiesMethods.CalculateDifficultyMetric(init_asteroidSpeed, limit_asteroidSpeed, diffRate);
                break;
            case 2:
                shooterSpawnRate = UtilitiesMethods.CalculateDifficultyMetric(init_ShooterSpawnRate, limit_ShooterSpawnRate, diffRate);
                shooterSpeed = UtilitiesMethods.CalculateDifficultyMetric(init_ShooterSpeed, limit_ShooterSpeed, diffRate);
                shooterWaitTime = UtilitiesMethods.CalculateDifficultyMetric(init_ShooterWaitTime, limit_ShooterWaitTime, diffRate);
                shooterTimeBtwShoots = UtilitiesMethods.CalculateDifficultyMetric(init_ShooterTimeBtwShoots, limit_ShooterTimeBtwShoots, diffRate);
                break;
            case 3:
                missileSpawnRate = UtilitiesMethods.CalculateDifficultyMetric(init_MissileSpawnRate, limit_MissileSpawnRate, diffRate);
                missileSpeed = UtilitiesMethods.CalculateDifficultyMetric(init_MissileSpeed, limit_MissileSpeed, diffRate);
                break;
            case 4:
                guidedMissileTimer = UtilitiesMethods.CalculateDifficultyMetric(init_GuidedMissileTimer, limit_GuidedMissileTimer, diffRate);
                guidedMissileSpeed = UtilitiesMethods.CalculateDifficultyMetric(init_GuidedMissileSpeed, limit_GuidedMissileSpeed, diffRate);
                break;
            default:
                break;
        }
    }

}
