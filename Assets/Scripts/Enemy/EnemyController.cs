using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;

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
                1f,
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (direction < 0f)
        {
            transform.localScale = new Vector3(
                -1f,
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
            rb.linearVelocity.y
        );
    }
}