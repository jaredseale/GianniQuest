using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGates : MonoBehaviour
{

    [SerializeField] bool yellow;
    [SerializeField] bool green;
    [SerializeField] bool red;
    [SerializeField] bool blue;
    [SerializeField] bool orange;
    [SerializeField] bool purple;

    [SerializeField] ParticleSystem particles1;
    [SerializeField] ParticleSystem particles2;
    [SerializeField] BoxCollider2D myCollider;

    private void Start() {
        if (yellow && PlayerPrefs.GetInt("BrokeYellowEgg") == 1) {
            particles1.gameObject.SetActive(false);
            particles2.gameObject.SetActive(false);
        }

        if (green && PlayerPrefs.GetInt("BrokeGreenEgg") == 1) {
            particles1.gameObject.SetActive(false);
            particles2.gameObject.SetActive(false);
        }

        if (red && PlayerPrefs.GetInt("BrokeRedEgg") == 1) {
            particles1.gameObject.SetActive(false);
            particles2.gameObject.SetActive(false);
        }

        if (blue && PlayerPrefs.GetInt("BrokeBlueEgg") == 1) {
            particles1.gameObject.SetActive(false);
            particles2.gameObject.SetActive(false);
        }

        if (orange && PlayerPrefs.GetInt("BrokeOrangeEgg") == 1) {
            particles1.gameObject.SetActive(false);
            particles2.gameObject.SetActive(false);
        }

        if (purple && PlayerPrefs.GetInt("BrokePurpleEgg") == 1) {
            particles1.gameObject.SetActive(false);
            particles2.gameObject.SetActive(false);
        }
    }

    void Update() {

        if (yellow && PlayerPrefs.GetInt("BrokeYellowEgg") == 1) {
            myCollider.enabled = false;
            particles1.Stop();
            particles2.Stop();
        }

        if (green && PlayerPrefs.GetInt("BrokeGreenEgg") == 1) {
            myCollider.enabled = false;
            particles1.Stop();
            particles2.Stop();
        }

        if (red && PlayerPrefs.GetInt("BrokeRedEgg") == 1) {
            myCollider.enabled = false;
            particles1.Stop();
            particles2.Stop();
        }

        if (blue && PlayerPrefs.GetInt("BrokeBlueEgg") == 1) {
            myCollider.enabled = false;
            particles1.Stop();
            particles2.Stop();
        }

        if (orange && PlayerPrefs.GetInt("BrokeOrangeEgg") == 1) {
            myCollider.enabled = false;
            particles1.Stop();
            particles2.Stop();
        }

        if (purple && PlayerPrefs.GetInt("BrokePurpleEgg") == 1) {
            myCollider.enabled = false;
            particles1.Stop();
            particles2.Stop();
        }

    }
}
