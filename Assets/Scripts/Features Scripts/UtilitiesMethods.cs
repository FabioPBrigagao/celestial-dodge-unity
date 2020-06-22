using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesMethods : MonoBehaviour{

    // Generates a random number except a specific one
    public int RandomRangeExcept(int min, int max, int previous, List<int> except){
        int number = 0;
        bool inList = true;
        bool found = false;
        while (inList){
            found = false;
            number = Random.Range(min, max);
            for (int i = 0; i < except.Count; i++){
                if (except[i] == number && previous != number){
                    found = true;
                }
                else if (previous == number){
                    found = true;
                }
            }
            if (found == false){
                inList = false;
            }
        }
        return number;
    }








}
