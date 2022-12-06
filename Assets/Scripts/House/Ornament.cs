using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornament : MonoBehaviour
{

    SpriteRenderer ornamentColor;

    List<Vector3> colors = new List<Vector3>() { 
        new Vector3(1f, 0f, 0f), //red
        new Vector3(1f, 0.5f, 0f), //orange
        new Vector3(1f, 1f, 0f), //yellow
        new Vector3(0.5f, 1f, 0f), //yellow green
        new Vector3(0f, 1f, 0f), //green
        new Vector3(0f, 1f, 0.5f), //aqua
        new Vector3(0f, 1f, 1f), //cyan
        new Vector3(0f, 0.5f, 1f), //light blue
        new Vector3(0f, 0f, 1f), //blue
        new Vector3(0.5f, 0f, 1f), //purple
        new Vector3(1f, 0f, 1f), //magenta
        new Vector3(1f, 0f, 0.5f) //hot pink
    };

    float speed = 2f;

    void Start() {
        ornamentColor = this.GetComponent<SpriteRenderer>();

        int color = Random.Range(0, colors.Count); //set the initial color
        ornamentColor.color = new Color(colors[color].x, colors[color].y, colors[color].z);

        StartCoroutine(IdleTime());
    }


    IEnumerator Vector3LerpCoroutine(GameObject obj, Color target, float waitTime) {
        Color curStartColor = ornamentColor.color;
        float elapsedTime = 0f;

        while (elapsedTime < waitTime) {
            
            ornamentColor.color = Color.Lerp(ornamentColor.color, target, elapsedTime / waitTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(IdleTime());
    }

    IEnumerator IdleTime() {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        int color = Random.Range(0, colors.Count);
        StartCoroutine(Vector3LerpCoroutine(this.gameObject, new Color(colors[color].x, colors[color].y, colors[color].z), speed));
    }
}
