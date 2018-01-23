using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour {

    public GameObject shootPFX;
    public GameObject collidePFX;
    public int scoreValue;

    private GamePlayController gameController;

    GameObject gameControllerObject;

    void Start()
    {
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GamePlayController>();
        }
        if(gameController == null)
        {
            Debug.Log("Cannot find game controller object for score updation !");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destroyer")
        {
            return;
        }
        Instantiate(shootPFX, other.transform.position, other.transform.rotation);
        if(other.tag == "Player")
        {
            Debug.Log("Game Over !!!");
            Instantiate(collidePFX, other.transform.position, other.transform.rotation);
            gameController = gameControllerObject.GetComponent<GamePlayController>();
            gameController.OnPlayerDestroy();
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);

    }
}
