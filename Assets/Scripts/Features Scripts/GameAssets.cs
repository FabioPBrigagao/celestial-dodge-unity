using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour{

    private static GameAssets _i;

    public static GameAssets i{
        get{
            if (_i == null) _i = (Instantiate(Resources.Load("Game Assets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Transform greenBoss;
    public Transform greenEnemyShooter;

    public Transform[] asteroidArray;

    public Transform missile;
    public Transform warningSign;

    public Transform greenNuke;
    public ParticleSystem nukeHitParticles;
    public Transform explosion;

    public Transform enemyBullet;
    public Transform playerBullet;
    
    public Material flash;
    public Material defaultSprite;
    public Transform bonusScore;
}













