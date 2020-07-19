using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    private Rigidbody2D rb;

    public static void GetFromPool(Vector2 position, ObjectPooling pool) {
        Quaternion rotation;
        if (position.x < 0) rotation = Quaternion.identity;
        else rotation = Quaternion.Euler(0, 180, 0);

        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = position;
        Obj.transform.rotation = rotation;
        Obj.SetActive(true);
    }

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        rb.velocity = transform.right * DifficultyController.instance.missileSpeed;
    }

    void OnDisable() {
        rb.velocity = transform.right * 0;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Destroy_Object") gameObject.SetActive(false);
    }
}