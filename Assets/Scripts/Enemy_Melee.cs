using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    public float damage;

    private bool facingRight = true;
    private Transform player;
    

    private void Awake()
    {
        damage += SpawnEnemies.increaseDamage;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        if (facingRight)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Player_Health>().TakeDamage(damage);
    }
}
