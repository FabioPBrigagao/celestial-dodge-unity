using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float explosionRadius;

    [Header("Cache Components")]
    public SpriteRenderer spr;

    private Color textColor = new Color(1, 1, 1, 1);

    public static GameObject GetFromPool(Vector2 position, ObjectPooling pool) {
        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = position;
        Obj.SetActive(true);
        return Obj;
    }

    void OnEnable() {
        textColor.a = 1;
    }

    void Update() {
        float vanishingSpeed = 1f;
        textColor.a -= vanishingSpeed * Time.deltaTime;
        spr.color = textColor;
        if (textColor.a <= 0) {
            gameObject.SetActive(false);
            textColor.a = 1;
        }
    }
}
