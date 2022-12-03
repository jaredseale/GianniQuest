using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    public GameObject currentWaypoint;
    //public GameObject targetWaypoint;
    public GameObject spawnWaypoint;
    public bool canEnter;
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

    [SerializeField] GameObject dollars;
    [SerializeField] GameObject helpButton;

    public string spawnPositionString = "saloon";
    public bool playerInTransit;
    public bool playerOnMajorWaypoint;
    public bool canMove; //for use in the overworld dialogue

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
        canMove = true;
        SetSpawnPosition();
    }

    void Update() {
        Move();
        playerOnMajorWaypoint = PlayerIsOnMajorWaypoint();
        SetNextSceneToLoad();
        if (FindObjectOfType<SpawnPosition>()) {
            FindObjectOfType<SpawnPosition>().overworldSpawnPosition = spawnPositionString;
        }
        LoadNextScene();
    }

    private void Move() {
        if (canMove) {
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
            currentWaypoint.name == "C1" ||
            currentWaypoint.name == "J" ||
            currentWaypoint.name == "N4" ||
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
            canEnter = true;
            switch (playerLocation) {

                case "S":
                    nextScene = "House Hallway";
                    spawnPositionString = "saloon";
                    spawn.setNextSpawn(13.33f, -1.91f);
                    break;

                case "N4":
                    nextScene = "Ethereal Ascent";
                    spawnPositionString = "ascent";
                    if (PlayerPrefs.GetString("EtherealAscentEntry") != "Open") {
                        canEnter = false;
                    }
                    spawn.setNextSpawn(8.52f, 1.23f);
                    break;

                case "C1":
                    nextScene = "Pizza Hell";
                    spawnPositionString = "pizza";
                    spawn.setNextSpawn(-7.87f, -3.77f);
                    break;

                case "T":
                    nextScene = "Sewers 1";
                    spawnPositionString = "sewer";
                    if (PlayerPrefs.GetString("SewersEntry") != "Open") {
                        canEnter = false;
                    }
                    PlayerPrefs.SetInt("Room5Button", 0); //prevents a softlock in the case the player exits the game before breaking the yellow egg
                    spawn.setNextSpawn(0.43f, 23f);
                    break;

                case "J":
                    nextScene = "Rancid Rick's";
                    spawnPositionString = "rick";
                    if (PlayerPrefs.GetString("RicksEntry") != "Open") {
                        canEnter = false;
                    }
                    spawn.setNextSpawn(25.27f, -3.69f);
                    break;

                case "A":
                    switch (PlayerPrefs.GetString("DateProgress")) {
                        case "Init":
                            nextScene = "Le Cul Puant Exterior";
                            break;
                        case "Interior":
                            nextScene = "Le Cul Puant Interior";
                            break;
                        case "Spaceship":
                            nextScene = "Spaceship";
                            break;
                        case "End":
                            nextScene = "Crashed Exterior";
                            break;
                        default:
                            Debug.Log("could not determine what scene to load");
                            break;
                    }

                    spawnPositionString = "LCP";
                    if (PlayerPrefs.GetString("LCPEntry") != "Open") {
                        canEnter = false;
                    }
                    break;

                case "V":
                    nextScene = "High School High";
                    spawnPositionString = "school";
                    spawn.setNextSpawn(4.37f, -0.5f);
                    if (PlayerPrefs.GetString("SchoolEntry") != "Open") {
                        canEnter = false;
                    }
                    break;

                case "W":
                    nextScene = "SNICO";
                    spawnPositionString = "snico";
                    if (PlayerPrefs.GetString("SNICOEntry") != "Open") {
                        canEnter = false;
                    }
                    break;
            }
        }
    }

    void LoadNextScene() {
        if (!playerInTransit) {
            if (Input.GetButtonDown("Jump") && playerOnMajorWaypoint) {
                if (!canEnter) {
                    //do dialogue stuff here
                    return;
                }
                playerInTransit = true;
                FindObjectOfType<Pause>().canPause = false;
                audioSource.PlayOneShot(SM64sound);
                FindObjectOfType<OverworldCamera>().ZoomToNewLocation(spawnPositionString);

                dollars.SetActive(false);
                helpButton.SetActive(false);

                LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
                levelLoader.LoadSceneWithDelay(nextScene, true);
            }
        }
    }
}
