using System.Collections;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float hp;
    public int score;

    public GameObject[] CoinDrops;
    
    public Material flashMaterial;
    private Material defaultMaterial;

    public float camShakeForce;
    public float camShakeDuration;
    public float timeStop;


    private bool moving;
    private int layer;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Rigidbody2D rb;
    

    private void Awake()
    {
        hp += SpawnEnemies.increaseHealth;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        anim = GetComponentInChildren<Animator>();

        layer = spriteRenderer.sortingOrder;
        defaultMaterial = spriteRenderer.material;

        StartCoroutine(isMoving());
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        StartCoroutine(Flash());

        CameraShake.Instance.ShakeCamera(camShakeForce, camShakeDuration);
        AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyHurt);
        StopTime.Instance.TimeStop(timeStop);
    }

    IEnumerator Flash()
    {
        spriteRenderer.material = flashMaterial;
        spriteRenderer.sortingOrder = 9;

        yield return new WaitForSeconds(timeStop);

        spriteRenderer.material = defaultMaterial;
        spriteRenderer.sortingOrder = layer;

        if (hp <= 0)
            StartCoroutine(Death());
    }

    IEnumerator isMoving()
    {
        while (true)
        {
            Vector2 currentPos = transform.position;

            yield return new WaitForSeconds(0.1f);

            if (currentPos == (Vector2)transform.position && moving)
                ChangeAnim();
            else if (currentPos != (Vector2)transform.position && !moving)
                ChangeAnim();
        }
    }

    void ChangeAnim()
    {
        moving = !moving;
        anim.SetBool("moving", moving);
    }

    IEnumerator Death()
    {
        int rand = Random.Range(0, CoinDrops.Length);

        spriteRenderer.sortingOrder = 9;
        anim.SetTrigger("die");

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        CameraShake.Instance.ShakeCamera(camShakeForce*1.5f, camShakeDuration);
        AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyDeath);

        yield return new WaitForSeconds(0.2f);

        Instantiate(CoinDrops[rand], transform.position, Quaternion.identity);

        Score.score += score;
        SpawnEnemies.enemiesOnScreen--;
        SpawnEnemies.enemiesDefeated++;

        Destroy(this.gameObject);
    }

}
