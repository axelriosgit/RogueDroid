using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text killCounterText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;

    private int enemiesDefeated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateKillCounter();
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        UpdateKillCounter();
    }
    public void GameOver()
    {
        Debug.Log("GAME OVER");

        if (finalScoreText != null)
        {
            finalScoreText.text = "ENEMIES DEFEATED: " + enemiesDefeated;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateKillCounter()
    {
        if (killCounterText != null)
        {
            killCounterText.text = "ENEMIES: " + enemiesDefeated;
        }
    }
}