using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController current;

    private Vector3 initialPosition;
    private float xOffSet;
    private float yOffset;
    private float shakeTimeCount;

    void Awake() => current = this; 

    // Shake Camera
    public IEnumerator ShakeCamera(float shakeTime, float magnetude){
        initialPosition = transform.position;
        shakeTimeCount = shakeTime;
        while (shakeTimeCount > 0){
            xOffSet = Random.Range(-0.1f, 0.1f) * magnetude;
            yOffset = Random.Range(-0.1f, 0.1f) * magnetude;
            transform.position = new Vector3(xOffSet,yOffset,initialPosition.z);
            shakeTimeCount -= Time.deltaTime;
            yield return null;
        }
        transform.position = initialPosition;
    }








}
