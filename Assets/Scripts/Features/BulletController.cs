using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed;
    public LayerMask deactivateLayer;

    [Header("Cache Components")]
    public Rigidbody2D rb;
    public BoxCollider2D col;

    public static void Fire(Transform transform, ObjectPooling pool) {
        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = transform.position;
        Obj.SetActive(true);
    }

    void OnEnable() {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(deactivateLayer)) gameObject.SetActive(false);
    }


    void Disable() {
        gameObject.SetActive(false);
    }

    void OnDisable() {
        rb.velocity = transform.up * 0;
    }

}
