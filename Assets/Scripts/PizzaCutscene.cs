using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCutscene : MonoBehaviour
{

    [SerializeField] GameObject titleScreen;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] Animator cameraAnim;
    [SerializeField] Animator textAnim;
    [SerializeField] AudioSource levelMusic;
    [SerializeField] GameObject pizzaGuy;

    private void Start() {
        FindObjectOfType<Player>().canMove = false;
        FindObjectOfType<Pause>().canPause = false;
        levelMusic.Stop();
        pizzaGuy.SetActive(false);
    }

    public void BeginCameraZoom() {
        cameraAnim.SetTrigger("beginZoom");
    }

    public void ShowTitle() {
        GetComponent<MeshRenderer>().enabled = false;
        titleScreen.SetActive(true);
    }

    public void LoadCredits() {
        textAnim.SetTrigger("textFadeOut");
        levelLoader.LoadSceneWithDelay("Credits", true);
    }
}
