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

    Vector3 saloonPos = new Vector3(38f, -19f, -10f); //all Zs should be -10f
    float saloonZoom = 3.5f;

    Vector3 schoolPos = new Vector3(-12f, -13f, -10f);
    float schoolZoom = 3.5f;

    Vector3 snicoPos = new Vector3(1f, -6.5f, -10f);
    float snicoZoom = 3.5f;

    Vector3 LCPPos = new Vector3(-15f, 3f, -10f);
    float LCPZoom = 3.5f;

    Vector3 rickPos = new Vector3(27f, -4.5f, -10f);
    float rickZoom = 3.5f;

    Vector3 sewerPos = new Vector3(13f, -16f, -10f);
    float sewerZoom = 3.5f;

    Vector3 pizzaPos = new Vector3(11f, 9f, -10f);
    float pizzaZoom = 5f;

    Vector3 ascentPos = new Vector3(37f, 7f, -10f);
    float ascentZoom = 6f;

    [SerializeField] Animator crossfade;


    void Start() {
        player = FindObjectOfType<OverworldPlayer>();
        startingPosString = player.spawnPositionString;

        switch (startingPosString) {
            case ("saloon"):
                cameraZoom = saloonZoom;
                cameraPos = saloonPos;
                break;

            case ("school"):
                cameraZoom = schoolZoom;
                cameraPos = schoolPos;
                break;

            case ("snico"):
                cameraZoom = snicoZoom;
                cameraPos = snicoPos;
                break;

            case ("LCP"):
                cameraZoom = LCPZoom;
                cameraPos = LCPPos;
                break;

            case ("rick"):
                cameraZoom = rickZoom;
                cameraPos = rickPos;
                break;

            case ("sewer"):
                cameraZoom = sewerZoom;
                cameraPos = sewerPos;
                break;

            case ("pizza"):
                cameraZoom = pizzaZoom;
                cameraPos = pizzaPos;
                break;

            case ("ascent"):
                cameraZoom = ascentZoom;
                cameraPos = ascentPos;
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
        StopCoroutine("LerpCameraToNormalZoom");

        switch (locationName) {
            case ("saloon"):
                cameraZoom = saloonZoom;
                cameraPos = saloonPos;
                break;

            case ("school"):
                cameraZoom = schoolZoom;
                cameraPos = schoolPos;
                break;

            case ("snico"):
                cameraZoom = snicoZoom;
                cameraPos = snicoPos;
                break;

            case ("LCP"):
                cameraZoom = LCPZoom;
                cameraPos = LCPPos;
                break;

            case ("rick"):
                cameraZoom = rickZoom;
                cameraPos = rickPos;
                break;

            case ("sewer"):
                cameraZoom = sewerZoom;
                cameraPos = sewerPos;
                break;

            case ("pizza"):
                cameraZoom = pizzaZoom;
                cameraPos = pizzaPos;
                break;

            case ("ascent"):
                cameraZoom = ascentZoom;
                cameraPos = ascentPos;
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
