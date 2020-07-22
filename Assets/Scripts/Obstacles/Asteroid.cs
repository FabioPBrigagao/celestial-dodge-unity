using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float speed;
    public LayerMask deactivateLayer;

    [Header("Cache Components")]
    public PolygonCollider2D col;

    private int randomBinaryNum;
    private float init_PrefabSpeed;
    private const float ROTATION_RATE = 0.3f;

    public static GameObject GetFromPool(Vector2 position, ObjectPooling pool) {
        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = position;
        Obj.SetActive(true);
        return Obj;
    }

    void Awake(){
        init_PrefabSpeed = speed;
    }

    void OnEnable() {
        SetVelocity();
    }

    void SetVelocity() {
        randomBinaryNum = Random.Range(0, 2);
        if (randomBinaryNum == 0) {
            speed = DifficultyController.instance.asteroidSpeed;
        } else speed = init_PrefabSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -1 * speed);
    }

    void Update() {
        Rotation();
    }

    void Rotation() {
        gameObject.GetComponent<Rigidbody2D>().rotation = gameObject.GetComponent<Rigidbody2D>().rotation + ROTATION_RATE;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(deactivateLayer)) gameObject.SetActive(false);
    }
}



