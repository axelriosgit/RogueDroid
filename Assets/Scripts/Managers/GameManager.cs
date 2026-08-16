using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text killCounterText;

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

    private void UpdateKillCounter()
    {
        if (killCounterText != null)
        {
            killCounterText.text = "ENEMIES: " + enemiesDefeated;
        }
    }
}