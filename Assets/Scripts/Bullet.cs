using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public GameObject particles;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!sprite.isVisible)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)transform.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.bulletHit);

            GameObject newParticles = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(newParticles, 0.1f);

            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.bulletHit);

            collision.GetComponent<Enemy_Health>().TakeDamage(damage);

            GameObject newParticles = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(newParticles,0.1f);

            Destroy(this.gameObject);
        }
    }
}
