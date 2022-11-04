using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreak : MonoBehaviour
{

    [SerializeField] bool yellowEgg;
    [SerializeField] bool greenEgg;
    [SerializeField] bool redEgg;
    [SerializeField] bool blueEgg;
    [SerializeField] bool orangeEgg;
    [SerializeField] bool purpleEgg;

    BoxCollider2D topOfEgg;
    Animator myAnim;
    AudioSource myAudio;
    Player myPlayer;
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject bird;
    [SerializeField] Animator eggGlowAnim;


    void Start() {
        topOfEgg = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        myPlayer = FindObjectOfType<Player>();

        if (yellowEgg && PlayerPrefs.GetInt("BrokeYellowEgg") == 1) {
            Destroy(gameObject);
        }

        if (greenEgg && PlayerPrefs.GetInt("BrokeGreenEgg") == 1) {
            Destroy(gameObject);
        }

        if (redEgg && PlayerPrefs.GetInt("BrokeRedEgg") == 1) {
            Destroy(gameObject);
        }

        if (blueEgg && PlayerPrefs.GetInt("BrokeBlueEgg") == 1) {
            Destroy(gameObject);
        }

        if (orangeEgg && PlayerPrefs.GetInt("BrokeOrangeEgg") == 1) {
            Destroy(gameObject);
        }

        if (purpleEgg && PlayerPrefs.GetInt("BrokePurpleEgg") == 1) {
            Destroy(gameObject);
        }

    }

    void Update() {
        if (topOfEgg.IsTouchingLayers(LayerMask.GetMask("PlayerFeet"))){
            topOfEgg.enabled = false;
            particles.Stop();
            myAnim.SetTrigger("breakEgg");
            eggGlowAnim.SetTrigger("breakEgg");
            myAudio.Play();
            bird.SetActive(true);

            //bounce off the egg when you jump on it
            myPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(myPlayer.GetComponent<Rigidbody2D>().velocity.x, 10f);

            if (yellowEgg) {
                PlayerPrefs.SetInt("BrokeYellowEgg", 1);
            }

            if (greenEgg) {
                PlayerPrefs.SetInt("BrokeGreenEgg", 1);
            }

            if (redEgg) {
                PlayerPrefs.SetInt("BrokeRedEgg", 1);
            }

            if (blueEgg) {
                PlayerPrefs.SetInt("BrokeBlueEgg", 1);
                PlayerPrefs.SetString("EtherealAscentEntry", "Done");
            }

            if (orangeEgg) {
                PlayerPrefs.SetInt("BrokeOrangeEgg", 1);
            }

            if (purpleEgg) {
                PlayerPrefs.SetInt("BrokePurpleEgg", 1);
            }
        }
        
    }
}
