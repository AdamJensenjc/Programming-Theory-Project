using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public GameObject ballPrefab;
    public Vector3 spawnPosition;
    public Counter counter;
    public GameObject net;
    private AudioSource gameAudio;
    public GameObject scoreParticle;
    public AudioClip scoreSound;
    public AudioClip gameOverSound;

    private int score;
    private int gameTime = 60;
    private float spawnCD = 1f;
    public bool isGameActive { get; private set; }

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(Difficulty difficulty)
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        SetDifficulty(difficulty);
        titleScreen.SetActive(false);
        timer = gameTime;
        timeText.text = "Time: " + timer;
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                net.AddComponent<EasyNet>();
                break;
            case Difficulty.Medium:
                net.AddComponent<MediumNet>();
                break;
            case Difficulty.Hard:
                net.AddComponent<HardNet>();
                break;
        }
    }

    // While game is active spawn a ball
    public void SpawnBall()
    {
        StartCoroutine(SpawnNextBall());
    }

    IEnumerator SpawnNextBall()
    {
        yield return new WaitForSeconds(spawnCD);
        Instantiate(ballPrefab, spawnPosition, ballPrefab.transform.rotation);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        if (isGameActive)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
            if (scoreToAdd > 0)
            {
                Instantiate(scoreParticle, counter.transform.position, scoreParticle.transform.rotation);
                gameAudio.PlayOneShot(scoreSound, 1f);
            }
        }
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameAudio.PlayOneShot(gameOverSound, 1f);
        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private int timer;
    private float timeTick = 1;
    private void Update()
    {
        if (isGameActive)
        {
            timeTick -= Time.deltaTime;
            if (timeTick <= 0)
            {
                timer -= 1;
                timeTick = 1;
                timeText.text = "Time: " + timer;
                if (timer <= 0)
                {
                    GameOver();
                }
            }
        }
    }
}
