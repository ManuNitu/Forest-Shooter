using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Health : MonoBehaviour
{
    public GameObject[] CoinDrops;
    public float Hp;
    public float camShakeForce;
    public float camShakeDuration;
    public float hurtDuration;
    public int score;
    private Material normal;
    public Material flash;
    private int layer;
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rb;
    private bool moving;
    private void Awake()
    {
        Hp += SpawnEnemies.increaseHealth;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        layer = sprite.sortingOrder;
        anim = GetComponentInChildren<Animator>();
        normal = sprite.material;
        StartCoroutine(isMoving());
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
        StartCoroutine(Flash());
        CameraShake.Instance.ShakeCamera(camShakeForce, camShakeDuration);
        AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyHurt);
        StopTime.Instance.TimeStop(hurtDuration);
    }
    IEnumerator Flash()
    {
        sprite.material = flash;
        sprite.sortingOrder = 9;
        yield return new WaitForSeconds(hurtDuration);
        sprite.material = normal;
        sprite.sortingOrder = layer;
        if (Hp <= 0)
            StartCoroutine(Death());
    }
    IEnumerator isMoving()
    {
        while (true)
        {
            Vector2 currentPos = transform.position;
            yield return new WaitForSeconds(0.1f);
            if ((currentPos == (Vector2)transform.position && moving))
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
        sprite.sortingOrder = 9;
        int rand = Random.Range(0, CoinDrops.Length);
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
