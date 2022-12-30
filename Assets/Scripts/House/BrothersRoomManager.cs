using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrothersRoomManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer roomSpriteRenderer;
    [SerializeField] Sprite sewersStateSprite;
    [SerializeField] Sprite sewersStateSprite2;
    [SerializeField] GameObject floorCollider;

    void Start() {
        if (PlayerPrefs.GetInt("Room11Wall") == 1) {
            roomSpriteRenderer.sprite = sewersStateSprite;
            floorCollider.SetActive(false);
        } 
        
        if (PlayerPrefs.GetString("SewersEntry") == "Done") {
            roomSpriteRenderer.sprite = sewersStateSprite2;
            floorCollider.SetActive(true);
        }

    }

}
