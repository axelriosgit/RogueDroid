using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TMP_Text waveCounterText;

    [Header("Waves")]
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float waveDelay = 3f;

    [Header("Difficulty")]
    [SerializeField] private int additionalEnemiesPerWave = 1;
    [SerializeField] private float spawnDelayDecrease = 0.1f;
    [SerializeField] private float minimumSpawnDelay = 0.4f;

    private int currentWave = 0;
    private int enemiesSpawned;
    private int enemiesAlive;
    private int enemiesThisWave;

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesSpawned = 0;

        if (waveCounterText != null)
        {
            waveCounterText.text = "WAVE: " + currentWave;
        }

        enemiesThisWave =
            enemiesPerWave +
            (currentWave - 1) * additionalEnemiesPerWave;

        float currentSpawnDelay =
            Mathf.Max(
                minimumSpawnDelay,
                spawnDelay - (currentWave - 1) * spawnDelayDecrease
            );

        Debug.Log(
            "Wave " + currentWave +
            " started! Enemies: " + enemiesThisWave
        );

        InvokeRepeating(
            nameof(SpawnEnemy),
            0f,
            currentSpawnDelay
        );
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= enemiesThisWave)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Instantiate(
            enemyPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        enemiesSpawned++;
        enemiesAlive++;
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 &&
            enemiesSpawned >= enemiesThisWave)
        {
            Invoke(nameof(StartNextWave), waveDelay);
        }
    }
}