using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gun : MonoBehaviour
{
    public float bulletsNumber;
    public float shootDistance;
    public float minShootCooldown;
    public float maxShootCooldown;
    public float camShakeForce;
    public float camShakeDuration;
    private float currentShootCooldown;
    public GameObject bullet;
    public Transform shootPoint;
    public AudioSource sound;

    private Transform enemy;
    private Transform player;
    private void Awake()
    {
        minShootCooldown -= SpawnEnemies.reduceCooldown;
        maxShootCooldown -= SpawnEnemies.reduceCooldown;
        currentShootCooldown = Random.Range(minShootCooldown, maxShootCooldown);
        enemy = gameObject.GetComponentInParent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        if(Vector2.Distance(transform.position,player.position) < shootDistance)
        {
            if (currentShootCooldown <= 0)
            {
                StartCoroutine(Shoot());
                currentShootCooldown = Random.Range(minShootCooldown, maxShootCooldown);
            }
            else
                currentShootCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Vector3 difference = player.position - transform.position;
        difference.Normalize();
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotz);
        if (rotz < -90 || rotz > 90)
        {
            if (enemy.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotz);
            }
            else if (enemy.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotz);
            }
        }
    }
    IEnumerator Shoot()
    {
        for(int i = 0; i < bulletsNumber; i++)
        {
            CameraShake.Instance.ShakeCamera(camShakeForce, camShakeDuration);
            AudioManager.Instance.PlaySfx(sound);
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
