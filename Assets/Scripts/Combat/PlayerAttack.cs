using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackDuration = 0.15f;
    [SerializeField] private float attackCooldown = 0.3f;

    [Header("Air Attack")]
    [SerializeField] private float airAttackHeight = 0.5f;

    private float lastAttackTime;
    private PlayerController playerController;
    private Vector3 originalHitboxPosition;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        if (attackHitbox != null)
        {
            originalHitboxPosition = attackHitbox.transform.localPosition;
        }
    }

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
        UpdateAttackDirection();

        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false);
    }

    private void UpdateAttackDirection()
    {
        if (attackHitbox == null || playerController == null)
        {
            return;
        }

        Vector3 position = originalHitboxPosition;

        if (!playerController.IsGrounded)
        {
            // Ataque hacia abajo mientras estamos en el aire.
            position.x = 0f;
            position.y = originalHitboxPosition.y - 0.8f;
        }
        else
        {
            // Ataque normal izquierda/derecha.
            position.x =
                Mathf.Abs(originalHitboxPosition.x) *
                playerController.FacingDirection;

            position.y = originalHitboxPosition.y;
        }

        attackHitbox.transform.localPosition = position;
    }
}