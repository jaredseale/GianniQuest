using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Order State")]
public class OrderState : ScriptableObject
{
    public int orderNumber;
    public Sprite vehicleSprite;
    public AudioClip orderAudio;
    public string methodToExecuteAfterOrder;

    void Start() {
	
        
    }

    void Update() {
	
        
    }
}
