using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackDuration = 0.15f;
    [SerializeField] private float attackCooldown = 0.5f;

    private float lastAttackTime;

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
        {
            return;
        }

        lastAttackTime = Time.time;

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false);
    }
}