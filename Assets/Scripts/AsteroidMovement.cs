using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    public EditorWaypoints pathToFollow;
    public float speed;
    public int currentWayPointID = 0;
    public float reachDistance = 1.0f;
    Vector3 last_position;
    Vector3 current_position;

    public bool isCurveSpawn = false;

	void Start () {
        if(GamePlayController.waveSwitchCount <= 2)
        {
            // Move in the forward direction (i.e. come down from the top of the screen)
            GetComponent<Rigidbody>().velocity = -(transform.forward * speed);
        }
        else
        {
            // Move along the waypoints           
            last_position = transform.position;
            isCurveSpawn = true;
        }
	}

    void Update()
    {
        if (isCurveSpawn)
        {
            float distance = Vector3.Distance(pathToFollow.path_objs[currentWayPointID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[currentWayPointID].position, Time.deltaTime * speed);

            if(distance <= reachDistance)
            {
                currentWayPointID++;
            }

            if(currentWayPointID > pathToFollow.path_objs.Count)
            {
                currentWayPointID = 0;
            }

        }
    }
}
