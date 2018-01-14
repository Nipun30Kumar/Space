using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    public float speed;

	void Start () {
        if(GamePlayController.waveSwitchCount <= 4)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else
        {
            // Move along the waypoints
        }
	}
}
