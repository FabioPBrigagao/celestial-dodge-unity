using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public static ScoreController instance;

    public float highestDifficultyLevelScore;
    public Text scoreText;
    public Text highscoreText;
    public GameObject newHighscore;
    public GameObject[] Buttons;

    [HideInInspector] public float difficultyRate;
    [HideInInspector] public int bonus;
    private float score;
    private Player playerScript;
    private bool gameover = false;

    const int SCORE_RATE = 2;

    void Awake() => instance = this;

    void Start() {
        highscoreText.text = "HIGHSCORE :  " + PlayerPrefs.GetFloat("Highscore", 0);
        playerScript = WaveController.instance.playerScript;
    }

    void Update() {
        if (playerScript.playerActive) {
            CalculateScore();
        } else if (!gameover && score > 0) {
            gameover = true;
            CheckHighscoreAndRestart();
        }
    }

    void CalculateScore() {
        if (highestDifficultyLevelScore > score) {
            difficultyRate = 1 - ((highestDifficultyLevelScore - score) / highestDifficultyLevelScore);
        }
        if (bonus > 0) {
            score += bonus;
            bonus = 0;
        }
        score += (Time.deltaTime * SCORE_RATE);
        score = Mathf.Round(score * 100.0f) / 100.0f;
        scoreText.text = "SCORE :  " + score;
    }

    void CheckHighscoreAndRestart() {
        if (PlayerPrefs.GetFloat("Highscore", 0) < score) {
            PlayerPrefs.SetFloat("Highscore", score);
            highscoreText.text = "HIGHSCORE :  " + score;
            newHighscore.SetActive(true);
        }
        foreach (var item in Buttons) item.SetActive(true);
    }
}
