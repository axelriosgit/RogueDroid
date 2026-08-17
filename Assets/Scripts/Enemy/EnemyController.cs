using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
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
        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            return;
        }

        if (player == null)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float direction = Mathf.Sign(
            player.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );
    }

    public void ApplyKnockback(float force)
    {
        if (player == null)
            return;

        float direction = Mathf.Sign(
            transform.position.x - player.position.x
        );

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            new Vector2(direction * force, 2f),
            ForceMode2D.Impulse
        );

        knockbackTimer = knockbackDuration;
    }
}