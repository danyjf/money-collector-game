using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;

    private Vector3 offset = new Vector3(0, 30, 0);

    void Start() {
        
    }
	
    void LateUpdate() {
        if(player)
            transform.position = player.transform.position + offset;
    }
}
