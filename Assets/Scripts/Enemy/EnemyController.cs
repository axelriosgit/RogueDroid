using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Knockback")]
    [SerializeField] private float knockbackDuration = 0.15f;

    private Transform player;
    private Rigidbody2D rb;
    private float knockbackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            UpdateFacing(Mathf.Sign(rb.linearVelocity.x));
            return;
        }

        float direction = Mathf.Sign(
            player.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );

        UpdateFacing(direction);
    }

    private void UpdateFacing(float direction)
    {
        if (direction > 0f)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (direction < 0f)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    public void ApplyKnockback(float force)
    {
        if (rb == null || player == null)
        {
            return;
        }

        float direction = Mathf.Sign(
            transform.position.x - player.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * force,
            0f
        );

        knockbackTimer = knockbackDuration;
    }
}