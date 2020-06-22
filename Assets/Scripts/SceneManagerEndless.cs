using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerEndless : MonoBehaviour {

    public static SceneManagerEndless current;

    //Public Variables
    public float waveDuration;
    public float asteroidSpawnRate;
    public float enemySpawnRate;
    public float missileSpawnRate;
    public GameObject player;
    public Transform[] spawnPos;
    public Transform[] destroyPos;
    public Text scoreText;
    public Text highscoreText;
    public GameObject newHighscore;
    public GameObject[] Buttons;

    //Waves Private Variables
    [HideInInspector]
    public int waveNumber;
    private float countWaveDuration;
    private float score;
    private int bonusScore;
    private GameObject[] obstaclesArray;
    private int previousWave;
    private bool addBonus;
    private List<int> waveCount;

    //Asteroids Private Variables
    private int randomAsteroid;
    private float startSpawnRateAsteroid;

    //Enemy Private Variables
    private float startSpawnRateEnemy;

    //Missile Private Variables
    private bool aimPlayer;
    private float startSpawnRateMissile;

    //Nuke Private Variables
    private bool newWave;
    private int nukeCountPerWave;

    //Constants
    private const float MIN_ASTEROID_SPAWN_RATE = 0.3f;
    private const float MIN_ENEMY_SPAWN_RATE = 2.5f;
    private const float MIN_MISSILE_SPAWN_RATE = 0.2f;
    private const int SCORE_RATE = 2;
    private const float ABS_MAX_X_TOP_SPAWN = 7;
    private const float ABS_MAX_Y_SIDE_SPAWN = 13;
    private const int TOTAL_NUMBER_OF_WAVES = 4;

    void Awake() => current = this;

    void Start(){
        startSpawnRateAsteroid = asteroidSpawnRate;
        startSpawnRateEnemy = enemySpawnRate;
        startSpawnRateMissile = missileSpawnRate;
        highscoreText.text = "HIGHSCORE :  " + PlayerPrefs.GetFloat("Highscore",0); //Print score in the UI Canvas
        waveCount = new List<int>();
    }

    void Update(){
        if (player == null || player.GetComponent<PlayerController>().startPos == true){ //Begin when player is on position  
            WaveManager();
            switch(waveNumber){
                case 1:
                    Asteroids();
                    previousWave = 1;
                    break;
                case 2:
                    GreenEnemyShooters();
                    previousWave = 2;
                    break;
                case 3:
                    Missiles();
                    previousWave = 3;
                    break;
                case 4:
                    EnemyNukes();
                    previousWave = 4;
                    break;
                default:
                    break;
            }
            //While player is still alive
            if (player != null){ 
                CalculateScore();
                IncreaseDifficulty();
            } //If player is destroid
            else{ 
                CheckHighscoreAndRestart(); 
            }
        } 
    }

    // Method that manages wave time countdown and checks if obstacles are still in the scene
    // A new wave starts only when all obstacles have been destroid
    // Wave types are selected randomly
    void WaveManager(){
        obstaclesArray = GameObject.FindGameObjectsWithTag("Obstacle"); 
        if (obstaclesArray.Length == 0 && countWaveDuration <= 0){
            if (waveCount.Count == TOTAL_NUMBER_OF_WAVES){
                waveCount.Clear();
            }else{
                waveNumber = GetComponent<UtilitiesMethods>().RandomRangeExcept(1, 5, previousWave, waveCount);
                countWaveDuration = waveDuration;
                waveCount.Add(waveNumber);
                newWave = true;
            }
        }else{
            countWaveDuration -= Time.deltaTime;
            newWave = false;
        }
    }

    //Spawn random asteroids into the scene
    void Asteroids(){
        randomAsteroid = Random.Range(0, GameAssets.i.asteroidArray.Length);
        if(newWave){
            InvokeRepeating("AsteroidSpawn", 0f, asteroidSpawnRate);
            newWave = false;
        }
        if(countWaveDuration <= 0){
            CancelInvoke();
        }
    }
    void AsteroidSpawn(){
        Vector2 topSpawnPosition = new Vector2((float)Random.Range(-ABS_MAX_X_TOP_SPAWN, ABS_MAX_X_TOP_SPAWN), spawnPos[0].position.y);
        Asteroid.Create(randomAsteroid, topSpawnPosition);
    }

    void GreenEnemyShooters(){
        if (newWave){
            InvokeRepeating("GreenEnemyShooterSpawn", 0f, enemySpawnRate);
            newWave = false;
        }
        if (countWaveDuration <= 0){
            CancelInvoke();
        }
    }
    void GreenEnemyShooterSpawn(){
        Vector2 topSpawnPosition = new Vector2((float)Random.Range(-ABS_MAX_X_TOP_SPAWN, ABS_MAX_X_TOP_SPAWN), spawnPos[0].position.y);
        GreenEnemyShooter.Create(topSpawnPosition);
    }

    void Missiles(){
        if (newWave){
            InvokeRepeating("MissileSpawn", 0f, missileSpawnRate);
            newWave = false;
        }
        if (countWaveDuration <= 0){
            CancelInvoke();
        }

    }
    void MissileSpawn(){
        float tempPosY = 0;
        if (aimPlayer == false){
            tempPosY = (float)Random.Range(-ABS_MAX_Y_SIDE_SPAWN, ABS_MAX_Y_SIDE_SPAWN);
            aimPlayer = true;
        }else if (player != null){
            tempPosY = player.transform.position.y;
            aimPlayer = false;
        }
        int tempPos = Random.Range(1, 3); // If 1, spawns on the right; If 2, spawns on the left
        Vector2 spawnPosition = new Vector2(spawnPos[tempPos].position.x,tempPosY) ;
        Missile.GetFromPool(spawnPosition);   
    }

    void EnemyNukes(){
        if (newWave){
            InvokeRepeating("EnemyNukeSpawn", 0f, enemySpawnRate);
        }
        if (countWaveDuration <= 0){
            CancelInvoke();
        }
    }
    void EnemyNukeSpawn(){
        int tempPos;
        tempPos = Random.Range(0, 3); // If 0, spawns on the top; If 1, spawns on the right; If 2, spawns on the left
        if (newWave){
            nukeCountPerWave += 1;
            newWave = false;
        }
       if(obstaclesArray.Length < nukeCountPerWave){
            GreenNuke.Create(spawnPos[tempPos]);
        }
    }

    public void AddBonusScore(int bonus){
        bonusScore = bonus;
        addBonus = true; 
    }

    void CalculateScore(){ 
        if(addBonus){
            score += bonusScore;
        }
        addBonus = false;
        score += (Time.deltaTime * SCORE_RATE);
        score = Mathf.Round(score * 100.0f) / 100.0f; //Round to the second decimal point
        scoreText.text = "SCORE :  " + score;
    }

    //This method increase the games difficulty depending on the number of points
    void IncreaseDifficulty(){
        if(waveNumber == 1){
            if (asteroidSpawnRate >= MIN_ASTEROID_SPAWN_RATE){ // Spawn Rate can't go below 0.4 or else it's impossible to stay alive
                asteroidSpawnRate = startSpawnRateAsteroid - (score / 1000);
            }
        }
        if (waveNumber == 2){
            if (enemySpawnRate >= MIN_ENEMY_SPAWN_RATE){ 
                enemySpawnRate = startSpawnRateEnemy - (score / 1000);
            }
        }
        if (waveNumber == 3){
            if (missileSpawnRate >= MIN_MISSILE_SPAWN_RATE){ 
                missileSpawnRate = startSpawnRateMissile - (score / 10000);
            }
        }
    }

    //Checks if the score was higher than highscore; if so, the score is assigned as new highscore
    //Display restart and menu buttons
    void CheckHighscoreAndRestart(){
        if (player == null && PlayerPrefs.GetFloat("Highscore",0) < score){
            PlayerPrefs.SetFloat("Highscore", score); //Set score as new highscore
            highscoreText.text = "HIGHSCORE :  " + score; //Print score in the UI Canvas
            newHighscore.SetActive(true);
        }
        foreach (var item in Buttons) item.SetActive(true);
    }

    //Buttons Method: Restart
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Buttons Method: Return to Menu
    public void ReturnMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public float GetScore(){
        return score;
    }
}
