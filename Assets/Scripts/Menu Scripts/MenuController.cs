using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{

    public Text highscore;

    void Awake(){
        highscore.text = "HIGHSCORE :  " + PlayerPrefs.GetFloat("Highscore", 0);
    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
