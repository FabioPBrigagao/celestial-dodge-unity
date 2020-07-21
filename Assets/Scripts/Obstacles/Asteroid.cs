using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float speed;
    public LayerMask deactivateLayer;

    [Header("Cache Components")]
    public PolygonCollider2D col;

    private int randomBinaryNum;
    private const float ROTATION_RATE = 0.3f;

    public static GameObject Create(int asteroidIndex, Vector2 position) {
        Transform transform = Instantiate(GameAssets.i.asteroidArray[asteroidIndex], position, Quaternion.identity);
        return transform.gameObject;
    }

    void Start() {
        SetVelocity();
    }

    void SetVelocity() {
        randomBinaryNum = Random.Range(0, 2);
        if (randomBinaryNum == 0) {
            speed = DifficultyController.instance.asteroidSpeed;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -1 * speed);
    }

    void Update() {
        Rotation();
    }

    void Rotation() {
        gameObject.GetComponent<Rigidbody2D>().rotation = gameObject.GetComponent<Rigidbody2D>().rotation + ROTATION_RATE;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(deactivateLayer)) Destroy(gameObject);
    }
}



