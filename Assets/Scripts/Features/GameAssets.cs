using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour{

    private static GameAssets instance;

    public static GameAssets i{
        get{
            if (instance == null) instance = (Instantiate(Resources.Load("Game Assets")) as GameObject).GetComponent<GameAssets>();
            return instance;
        }
    }
    
    public Transform explosion;
    public Transform bonusScore;
    public Transform enemyBullet;
    public Transform playerBullet;
    
    public Material flash;
    public Material defaultSprite;

}













