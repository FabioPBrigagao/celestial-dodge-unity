using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilitiesMethods {

    public static int RandomIntegerException(int min, int max, int previous, List<int> except) {
        int number = 0;
        bool inList = true;
        bool found = false;
        while (inList) {
            found = false;
            number = Random.Range(min, max);
            for (int i = 0; i < except.Count; i++) {
                if (except[i] == number || previous == number) {
                    found = true;
                } 
            }
            if (found == false) {
                inList = false;
            }
        }
        return number;
    }

    public static float CalculateDifficultyMetric(float initial, float limit, float rate) {
        return initial - ((initial - limit) * rate);
    }

    public static Vector2 XYRandomVector3(float xMin, float xMax, float yMin, float yMax, float z) {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        return new Vector3(x, y, z);
    }

    public static int CheckEnableList(List<GameObject> list) {
        int count = 0;
        foreach (var obstacle in list) {
            if (obstacle != null) {
                if (obstacle.activeSelf) {
                    count++;
                }
            }
        }
     return count;
    }


}
