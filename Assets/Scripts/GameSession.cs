using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    // Singleton pattern in Awake()

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    

    void Start() 
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public IEnumerator ProcessPlayerDeath()
    {
        yield return new WaitForSecondsRealtime(2);
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();

        // make sure canvas is child of game session, so that lives properly decrement every death
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
    }

}
