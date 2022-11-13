using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightToLeftUFO : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        int colorPicker = Random.Range(0, 4);

        if (colorPicker == 0) {
            mySpriteRenderer.color = new Color(Random.Range(0f, 0.5f), 1f, 1f);
        } else if (colorPicker == 1) {
            mySpriteRenderer.color = new Color(1f, Random.Range(0f, 0.5f), 1f);
        } else if (colorPicker == 2) {
            mySpriteRenderer.color = new Color(1f, 1f, Random.Range(0f, 0.5f));
        } else if (colorPicker == 3) {
            mySpriteRenderer.color = new Color(1f, 1f, 1f);
        }

        StartCoroutine(Vector3LerpCoroutine(gameObject, new Vector3(-65f, gameObject.transform.position.y, 0f), moveSpeed));
    }

    void Update() {
        if (gameObject.transform.position.x < -60f) {
            Destroy(gameObject);
        }
    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed) {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (obj.transform.position != target) {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f) {
            col.gameObject.transform.SetParent(gameObject.transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        col.gameObject.transform.parent = null;
    }
}
