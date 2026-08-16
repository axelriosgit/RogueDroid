using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Waves")]
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float waveDelay = 3f;

    private int currentWave = 0;
    private int enemiesSpawned;
    private int enemiesAlive;

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesSpawned = 0;

        Debug.Log("Wave " + currentWave + " started!");

        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnDelay);
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= enemiesPerWave)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        enemiesSpawned++;
        enemiesAlive++;
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 &&
            enemiesSpawned >= enemiesPerWave)
        {
            Invoke(nameof(StartNextWave), waveDelay);
        }
    }
}