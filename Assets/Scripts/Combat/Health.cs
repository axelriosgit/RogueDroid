using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Slider healthBar;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log(gameObject.name + " received " + damage +
                  " damage. Health: " + currentHealth);

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
        }

        Destroy(gameObject);
    }
}