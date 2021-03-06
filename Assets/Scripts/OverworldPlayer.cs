﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    public GameObject currentWaypoint;
    //public GameObject targetWaypoint;
    public GameObject spawnWaypoint;
    [SerializeField] GameObject saloonWaypoint;
    [SerializeField] GameObject schoolWaypoint;
    [SerializeField] GameObject snicoWaypoint;
    [SerializeField] GameObject LCPWaypoint;
    [SerializeField] GameObject rickWaypoint;
    [SerializeField] GameObject sewerWaypoint;
    [SerializeField] GameObject pizzaWaypoint;
    [SerializeField] GameObject ascentWaypoint;

    [SerializeField] string nextScene;
    SpawnPosition spawn;
    AudioSource audioSource;
    [SerializeField] AudioClip SM64sound;

    public string spawnPositionString = "saloon";
    public bool playerInTransit;
    public bool playerOnMajorWaypoint;

    private void Awake() {
        spawn = FindObjectOfType<SpawnPosition>();

        //this will cover the initial condition in which there has not been an overworld position set
        if (spawn.overworldSpawnPosition == "") { 
            spawn.overworldSpawnPosition = "saloon";
            playerOnMajorWaypoint = true;
        }

        spawnPositionString = FindObjectOfType<SpawnPosition>().overworldSpawnPosition;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerInTransit = false;
        SetSpawnPosition();
    }

    void Update() {
        Move();
        playerOnMajorWaypoint = PlayerIsOnMajorWaypoint();
        SetNextSceneToLoad();
        LoadNextScene();
    }

    private void Move() {
        if (Input.GetButtonDown("Right") && currentWaypoint.GetComponent<OverworldWaypoints>().rightPoints.Count > 0 && !playerInTransit) {
            StartCoroutine("MovePlayerToWaypoint", currentWaypoint.GetComponent<OverworldWaypoints>().rightPoints);
        }

        if (Input.GetButtonDown("Left") && currentWaypoint.GetComponent<OverworldWaypoints>().leftPoints.Count > 0 && !playerInTransit) {
            StartCoroutine("MovePlayerToWaypoint", currentWaypoint.GetComponent<OverworldWaypoints>().leftPoints);
        }

        if (Input.GetButtonDown("Up") && currentWaypoint.GetComponent<OverworldWaypoints>().upPoints.Count > 0 && !playerInTransit) {
            StartCoroutine("MovePlayerToWaypoint", currentWaypoint.GetComponent<OverworldWaypoints>().upPoints);
        }

        if (Input.GetButtonDown("Down") && currentWaypoint.GetComponent<OverworldWaypoints>().downPoints.Count > 0 && !playerInTransit) {
            StartCoroutine("MovePlayerToWaypoint", currentWaypoint.GetComponent<OverworldWaypoints>().downPoints);
        }
    }

    IEnumerator MovePlayerToWaypoint(List<GameObject> waypointList) {

        playerInTransit = true;

        foreach (GameObject waypoint in waypointList) {
            while (PlayerIsOnWaypoint(waypoint) == false) {
                transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
            currentWaypoint = waypoint;
        }
        playerInTransit = false;
    }

    private bool PlayerIsOnWaypoint(GameObject target) {
        if (Mathf.Abs(gameObject.transform.position.x - target.transform.position.x) > 0.01 
            || Mathf.Abs(gameObject.transform.position.y - target.transform.position.y) > 0.01) {
            return false;
        } else {
            return true;
        }
    }

    private bool PlayerIsOnMajorWaypoint() { //prevents the player from loading into a scene from a crossroads or on top of a mid-waypoint
        if (currentWaypoint.name == "A" ||
            currentWaypoint.name == "C" ||
            currentWaypoint.name == "J" ||
            currentWaypoint.name == "N" ||
            currentWaypoint.name == "S" ||
            currentWaypoint.name == "T" ||
            currentWaypoint.name == "V" ||
            currentWaypoint.name == "W") {
            return true;
        } else {
            return false;
        }
    }

    void SetSpawnPosition() {
        //this will allow the spawn position to be remembered through level loading,
        //taking us back to the correct spot on the overworld through the pause menu in levels.
        switch (spawnPositionString) {
            case "saloon":
                spawnWaypoint = saloonWaypoint;
                break;
            case "school":
                spawnWaypoint = schoolWaypoint;
                break;
            case "snico":
                spawnWaypoint = snicoWaypoint;
                break;
            case "LCP":
                spawnWaypoint = LCPWaypoint;
                break;
            case "rick":
                spawnWaypoint = rickWaypoint;
                break;
            case "sewer":
                spawnWaypoint = sewerWaypoint;
                break;
            case "pizza":
                spawnWaypoint = pizzaWaypoint;
                break;
            case "ascent":
                spawnWaypoint = ascentWaypoint;
                break;
        }

        gameObject.transform.position = spawnWaypoint.transform.position;
        currentWaypoint = spawnWaypoint;
    }

    void SetNextSceneToLoad() {
        if (!playerInTransit) {
            string playerLocation = currentWaypoint.gameObject.name;

            switch (playerLocation) {

                case "A":
                    nextScene = "House Hallway";
                    spawnPositionString = "saloon";
                    spawn.setNextSpawn(13.33f, -1.34f);
                    break;

                case "C":
                    nextScene = "Ethereal Ascent";
                    spawnPositionString = "ascent";
                    break;

                case "J":
                    nextScene = "Pizza Hell";
                    spawnPositionString = "pizza";
                    break;

                case "N":
                    nextScene = "The Sewers";
                    spawnPositionString = "sewer";
                    break;

                case "S":
                    nextScene = "Rancid Rick's";
                    spawnPositionString = "rick";
                    break;

                case "T":
                    nextScene = "Le Cul Puant";
                    spawnPositionString = "LCP";
                    break;

                case "V":
                    nextScene = "High School High";
                    spawnPositionString = "school";
                    break;

                case "W":
                    nextScene = "SNICO";
                    spawnPositionString = "snico";
                    break;
            }
        }
    }

    void LoadNextScene() {
        if (!playerInTransit) {
            if (Input.GetButtonDown("Jump") && playerOnMajorWaypoint) {
                playerInTransit = true;
                audioSource.PlayOneShot(SM64sound);
                FindObjectOfType<OverworldCamera>().ZoomToNewLocation(spawnPositionString);
                LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
                levelLoader.LoadSceneWithDelay(nextScene, true);
            }
        }
    }
}
