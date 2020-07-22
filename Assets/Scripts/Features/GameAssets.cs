using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {

    public static GameAssets i;

    void Awake() => i = this;

    public Transform bonusScore;
    public Material flash;
    public Material defaultSprite;

}













