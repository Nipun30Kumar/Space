using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour {

    public GameObject hazard;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public Button pauseButton;
    public Vector3 spawnValues;
    public int waveCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Vector3 curveWaveSpawnPosition;

    private int score;
    public Text scoreText;

    public static int waveSwitchCount;

    void Start()
    {
        score = 0;
        waveSwitchCount = 0;
        StartCoroutine("SpawnWaves");
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {

            yield return new WaitForSeconds(startWait);
            for (int i = 0; i <= waveCount; i++)
            {  
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if(waveSwitchCount <= 4)
                {
                    // Spawning of waves from top of the screen
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    waveSwitchCount++;
                }
                else
                {
                    // Curve spawning of waves from either side of the screen
                    waveSwitchCount = 0;
                    Instantiate(hazard, curveWaveSpawnPosition, Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score :" + score;
    }

    public void OnPlayerDestroy()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.5f;
    }

    public void OnClickHomeButton()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Gameplay");        
    }

    public void OnClickPause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseButton.interactable = false;
    }

    public void OnClickClosePausePanel()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.interactable = true;
    }
}
