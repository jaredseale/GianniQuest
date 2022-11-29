using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTriggerGate : MonoBehaviour
{

    [SerializeField] GameObject[] enemies;
    [SerializeField] bool gateOpen;
    [SerializeField] string gatePlayerPrefName;
    [SerializeField] Vector3 openPosition;
    [SerializeField] AudioSource chime;
    [SerializeField] AudioSource gateOpenSFX;

    void Start() {
        if (PlayerPrefs.GetInt(gatePlayerPrefName) == 0) {
            gateOpen = false;
        } else {
            gateOpen = true;
            gameObject.transform.position = openPosition;
        }
        
    }

    void Update() {
        if (!gateOpen) {
            CheckEnemies();
        }
        
    }

    private void CheckEnemies() {
        foreach (GameObject enemy in enemies) {
            if (enemy != null) {
                return;
            }
        }

        StartCoroutine(OpenGate());
    }

    IEnumerator OpenGate() {
        chime.Play();
        gateOpen = true;
        PlayerPrefs.SetInt(gatePlayerPrefName, 1);

        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveGate());
    }

    IEnumerator MoveGate() {
        gateOpenSFX.Play();
        Vector3 startPosition = gameObject.transform.position;
        float time = 0f;

        while (gameObject.transform.position != openPosition) {
            gameObject.transform.position = Vector3.Lerp(startPosition, openPosition, (time / Vector3.Distance(startPosition, openPosition)) * 4f);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
