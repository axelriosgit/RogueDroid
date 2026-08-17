using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDuration = 0.15f;

    private Transform player;
    private float lastAttackTime;
    private SpriteRenderer attackSprite;

    private void Awake()
    {
        Transform attackPoint = transform.Find("EnemyAttackPoint");

        if (attackPoint != null)
        {
            attackSprite = attackPoint.GetComponent<SpriteRenderer>();

            if (attackSprite != null)
            {
                attackSprite.enabled = false;
            }
        }
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Mathf.Abs(
            player.position.x - transform.position.x
        );

        if (distance <= attackRange &&
            Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;

        Health playerHealth = player.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        StartCoroutine(ShowAttack());
    }

    private IEnumerator ShowAttack()
    {
        if (attackSprite != null)
        {
            attackSprite.enabled = true;
        }

        yield return new WaitForSeconds(attackDuration);

        if (attackSprite != null)
        {
            attackSprite.enabled = false;
        }
    }
}