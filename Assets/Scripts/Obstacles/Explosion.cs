using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float explosionRadius;
    private Color textColor = new Color(1, 1, 1, 1);

    public static Explosion Create(Vector2 position) {
        Transform explosionTrans = Instantiate(GameAssets.i.explosion, position, Quaternion.identity);
        Explosion explosion = explosionTrans.GetComponent<Explosion>();
        return explosion;
    }

    public void Update() {
        float vanishingSpeed = 1f;
        textColor.a -= vanishingSpeed * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = textColor;
        if (textColor.a <= 0) {
            Destroy(gameObject);
        }
    }
}
