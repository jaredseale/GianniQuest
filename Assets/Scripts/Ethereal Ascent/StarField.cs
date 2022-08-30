using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{

    SpriteRenderer mySpriteRenderer;

    void Start() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        float randomAffector = Random.Range(0f, 0.5f);

        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - randomAffector, 
                gameObject.transform.localScale.y - randomAffector, 
                gameObject.transform.localScale.z - randomAffector);

        mySpriteRenderer.color = new Color(1f, 1f, Random.Range(0.3f, 1f), Random.Range(0f, 1f));

    }

}
