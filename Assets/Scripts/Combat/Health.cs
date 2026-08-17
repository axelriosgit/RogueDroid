using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private EnemyHealthBar healthBarPrefab;
    [SerializeField] private Transform healthBarAnchor;


    private int currentHealth;
    private EnemyHealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (CompareTag("Enemy") && healthBarPrefab != null && healthBarAnchor != null)
        {
            healthBar = Instantiate(
                healthBarPrefab,
                healthBarAnchor.position,
                Quaternion.identity,
                healthBarAnchor
            );

            healthBar.transform.localPosition = Vector3.zero;
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log(gameObject.name + " received " + damage +
                  " damage. Health: " + currentHealth);

        if (CompareTag("Enemy"))
        {
            EnemyController enemyController = GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.ApplyKnockback(knockbackForce);
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");

        if (CompareTag("Enemy"))
        {
            GameManager.Instance.EnemyDefeated();

            EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();

            if (spawner != null)
            {
                spawner.EnemyDefeated();
            }

            Destroy(gameObject);
        }
        else if (CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}