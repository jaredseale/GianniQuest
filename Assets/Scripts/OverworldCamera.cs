using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    OverworldPlayer player;
    [SerializeField] Camera camera;
    [SerializeField] float normalZoom = 18f;
    [SerializeField] Vector3 normalPos = new Vector3(10.7f, -5f, -10f);
    [SerializeField] float zoomOutTime = 1f;
    [SerializeField] float zoomInTime = 2f;
    public string startingPosString;
    [SerializeField] float cameraZoom;
    [SerializeField] Vector3 cameraPos;

    Vector3 saloonPos = new Vector3(-15f, 3f, -10f); //all Zs should be -10f
    float saloonZoom = 3.5f;

    [SerializeField] Animator crossfade;


    void Start() {
        player = FindObjectOfType<OverworldPlayer>();
        startingPosString = player.spawnPositionString;

        switch (startingPosString) { //gonna have to fill these out as i go
            case ("saloon"):
                cameraZoom = saloonZoom;
                cameraPos = saloonPos;
                break;
        }

        StartCoroutine("LerpCameraToNormalZoom");
    }

    IEnumerator LerpCameraToNormalZoom() {

        float timeElapsed = 0;

        while (timeElapsed < zoomOutTime) {
            camera.transform.position = Vector3.Lerp(cameraPos, normalPos, timeElapsed / zoomOutTime);
            camera.orthographicSize = Mathf.Lerp(cameraZoom, normalZoom, timeElapsed / zoomOutTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        camera.transform.position = normalPos;
        camera.orthographicSize = normalZoom;

    }

    public void ZoomToNewLocation(string locationName) {
        Debug.Log(zoomInTime);
        StopCoroutine("LerpCameraToNormalZoom");

        switch (locationName) {
            case ("saloon"): //fill the rest of these out as you add them
                cameraZoom = saloonZoom;
                cameraPos = saloonPos;
                break;
        }
        
        StartCoroutine("LerpCameraToOverworldLocation");
    }

    IEnumerator LerpCameraToOverworldLocation() {

        float timeElapsed = 0;
        normalPos = camera.transform.position;
        normalZoom = camera.orthographicSize;

        while (timeElapsed < zoomInTime) {
            camera.transform.position = Vector3.Lerp(normalPos, cameraPos, timeElapsed / zoomInTime);
            camera.orthographicSize = Mathf.Lerp(normalZoom, cameraZoom, timeElapsed / zoomInTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        crossfade.SetTrigger("doorTransition");
        camera.transform.position = cameraPos;
        camera.orthographicSize = cameraZoom;

    }

}
