using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public GameObject particles;
    private Rigidbody2D rb;
    private void Awake()
    {
        damage += SpawnEnemies.increaseDamage;
        rb = GetComponent<Rigidbody2D>();
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
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.bulletHit);
            collision.GetComponent<Player_Health>().TakeDamage(damage);
            GameObject newParticles = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(newParticles, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
