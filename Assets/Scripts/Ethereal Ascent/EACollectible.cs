using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EACollectible : MonoBehaviour
{

    BoxCollider2D myCollider;
    Player playerController;
    EAManager myEAManager;
    [SerializeField] string itemDescription;
    [SerializeField] SpriteRenderer itemSprite;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        playerController = FindObjectOfType<Player>();
        myEAManager = FindObjectOfType<EAManager>();
    }

    void Update() {
        Collect();
    }

    private void Collect() {
        if (Input.GetButtonDown("Up") //if 1) pressed up and 2) not on top of loading zone and 3) game not paused and 4) not already in dialogue and 5) is not in the air
            && myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))
            && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false
            && playerController.isOnGround) {

            myEAManager.itemCount += 1;
            myEAManager.itemCollectionText.text = itemDescription;
            myEAManager.itemCollectionIcon.sprite = itemSprite.sprite;
            myEAManager.ItemCollectionSlideIn();
            myEAManager.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        

    }
}
