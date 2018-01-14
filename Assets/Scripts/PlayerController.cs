using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed = 0.01f;
    public Boundary boundary;
    public float tilt;
    public float fireRate;
    public GameObject bolt;
    public Transform shootPoint;
    public AudioSource shootSound;

    private float nextFire;

    void Update() {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bolt, shootPoint.position, shootPoint.rotation);
            shootSound.Play();
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            // Move object across the plane.
            transform.Translate(touchDeltaPosition.x * speed,0, touchDeltaPosition.y * speed);
        }
    }

    void FixedUpdate() {
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //    GetComponent<Rigidbody>().velocity = movement * speed;

    //    //Vector3 playerPosition = GetComponent<Rigidbody>().position;
        GetComponent<Rigidbody>().position = new Vector3
            (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            
            );

    //    GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
