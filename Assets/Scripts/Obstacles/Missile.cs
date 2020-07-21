using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public LayerMask deactivateLayer;

    [Header("Cache Components")]
    public Rigidbody2D rb;
    public BoxCollider2D col;

    public static GameObject GetFromPool(Vector2 position, ObjectPooling pool) {
        Quaternion rotation;
        if (position.x < 0) rotation = Quaternion.identity;
        else rotation = Quaternion.Euler(0, 180, 0);

        GameObject Obj = pool.GetObjFromPool();
        Obj.transform.position = position;
        Obj.transform.rotation = rotation;
        Obj.SetActive(true);
        return Obj;
    }

    void OnEnable() {
        rb.velocity = transform.right * DifficultyController.instance.missileSpeed;
    }

    void OnDisable() {
        rb.velocity = transform.right * 0;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (col.IsTouchingLayers(deactivateLayer)) gameObject.SetActive(false);
    }
}