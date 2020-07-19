using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScore : MonoBehaviour{
    public float moveYSpeed;
    public float vanishTimer;
    public Transform position2;

    private TextMesh text;
    private Color textColor = new Color(1,1,1,1);

    public static BonusScore Create(Vector2 position, int bonusAmount){
        Transform bonusScoreTrans = Instantiate(GameAssets.i.bonusScore, position, Quaternion.identity);
        BonusScore bonusScore = bonusScoreTrans.GetComponent<BonusScore>();
        bonusScore.Setup(bonusAmount);
        return bonusScore;
    }

    private void Awake(){
        text = gameObject.GetComponent<TextMesh>();
    }

    public void Setup(int bonus){
        text.text = "+ " + bonus;
    }

    public void Update(){
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        vanishTimer -= Time.deltaTime;
        if(vanishTimer < 0 ){
            float vanishingSpeed = 5f;
            textColor.a -= vanishingSpeed * Time.deltaTime;
            text.color = textColor;
            if(textColor.a <= 0){
               Destroy(gameObject);
            }
        }


    }
}
