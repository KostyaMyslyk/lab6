using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI bestScoreText;

    public bool isGameOver = false;

    private const string BEST_KEY = "BEST_SCORE";

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);

        int savedBest = PlayerPrefs.GetInt(BEST_KEY, 0);

        if (bestScoreText != null)
            bestScoreText.text = "Best: " + savedBest;
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                RestartGame();
            }
        }
    }

    public void GameOver(int currentScore)
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(true);

        int savedBest = PlayerPrefs.GetInt(BEST_KEY, 0);

        if (currentScore > savedBest)
        {
            savedBest = currentScore;
            PlayerPrefs.SetInt(BEST_KEY, savedBest);
            PlayerPrefs.Save();
        }

        if (bestScoreText != null)
            bestScoreText.text = "Best: " + savedBest;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}