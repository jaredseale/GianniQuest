using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Attack5 : MonoBehaviour
{
    public int timeBetween;

    public GameObject[] lightGates;
    int[][] patternArray = new int[][] {
        new int[] {2, 1, 4, 0, 3, 5},
        new int[] {3, 2, 1, 4, 0, 5},
        new int[] {3, 5, 2, 4, 1, 0},
        new int[] {5, 0, 2, 4, 1, 3},
        new int[] {2, 1, 0, 3, 4, 5},
        new int[] {2, 3, 4, 0, 5, 1},
        new int[] {2, 0, 3, 1, 5, 4},
        new int[] {1, 5, 4, 3, 0, 2},
        new int[] {0, 1, 5, 2, 4, 3},
        new int[] {1, 2, 4, 0, 3, 5}
        };


    void Start() {
        StartCoroutine(AttackPattern1());
    }
    
    IEnumerator AttackPattern1() {
        int pattern = Random.Range(0, patternArray.Length);

        lightGates[patternArray[pattern][0]].SetActive(true);
        lightGates[patternArray[pattern][1]].SetActive(true);

        yield return new WaitForSeconds(timeBetween);

        lightGates[patternArray[pattern][2]].SetActive(true);
        lightGates[patternArray[pattern][3]].SetActive(true);

        yield return new WaitForSeconds(timeBetween);

        lightGates[patternArray[pattern][4]].SetActive(true);
        lightGates[patternArray[pattern][5]].SetActive(true);

        yield return new WaitForSeconds(timeBetween);

        StartCoroutine(AttackPattern2());
    }

    IEnumerator AttackPattern2() {
        int pattern = Random.Range(0, patternArray.Length);

        lightGates[patternArray[pattern][0]].SetActive(true);
        lightGates[patternArray[pattern][1]].SetActive(true);

        yield return new WaitForSeconds(timeBetween);

        lightGates[patternArray[pattern][2]].SetActive(true);
        lightGates[patternArray[pattern][3]].SetActive(true);

        yield return new WaitForSeconds(timeBetween);

        lightGates[patternArray[pattern][4]].SetActive(true);
        lightGates[patternArray[pattern][5]].SetActive(true);

        yield return new WaitForSeconds(timeBetween);
    }


}
