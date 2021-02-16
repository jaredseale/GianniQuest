using System.Collections;
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

    string spawnPositionString = "saloon";
    public bool playerInTransit;


    private void Awake() {

        if (FindObjectOfType<SpawnPosition>().overworldSpawnPosition == "") {
            FindObjectOfType<SpawnPosition>().overworldSpawnPosition = "saloon";
        }
        spawnPositionString = FindObjectOfType<SpawnPosition>().overworldSpawnPosition;
    }

    void Start() {
        playerInTransit = false;
        SetSpawnPosition();
    }

    void Update() {
        Move();
        
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

    void SetSpawnPosition() {
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

        gameObject.transform.position = spawnWaypoint.transform.position; //this might not work yet
        currentWaypoint = spawnWaypoint;
    }
}
