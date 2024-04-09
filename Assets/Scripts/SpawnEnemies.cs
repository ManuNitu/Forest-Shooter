using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public static int enemiesOnScreen;
    public static int enemiesDefeated;

    public static int increaseDamage;
    public static int increaseHealth;
    public static float reduceCooldown;


    public GameObject[] Enemies;

    public int minEnemiesToSpawn;
    public int maxEnemiesToSpawn;

    public float minCooldown;
    public float maxCooldown;
    private float currentCooldown;

    public int maxEnemiesOnScreen;
    public float spawnDistance;



    private int difficultyCounter;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (enemiesDefeated % 10 == 0 && enemiesDefeated / 10 <= 8 && difficultyCounter < enemiesDefeated/10)
        {
            increaseHealth += 5;
            increaseDamage += 1;
            reduceCooldown += 0.1f;
            minCooldown -= 0.1f;
            maxCooldown -= 0.1f;

            if (enemiesDefeated / 10 == 4)
                maxEnemiesOnScreen++;
            if (enemiesDefeated / 10 == 8)
                maxEnemiesToSpawn++;

            difficultyCounter++;
        }

        if (currentCooldown <= 0)
        {
            currentCooldown = Random.Range(minCooldown, maxCooldown);
            Spawn();
        }
        else
            currentCooldown -= Time.deltaTime;
    }

    void Spawn()
    {
        int enemiesNumber = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);

        if (enemiesOnScreen + enemiesNumber > maxEnemiesOnScreen)
            enemiesNumber = maxEnemiesOnScreen - enemiesOnScreen;

        for (int i = 0; i < enemiesNumber; i++)
        {
            int rand = Random.Range(0, Enemies.Length);
            Vector2 pos = player.transform.position;

            while (Vector2.Distance(player.transform.position, pos) < spawnDistance / 2)
            {
                Vector2 dirrection = Random.insideUnitCircle.normalized;
                pos = (dirrection * spawnDistance) + (Vector2)player.transform.position;
                pos.x = Mathf.Clamp(pos.x, -48f, 48f);
                pos.y = Mathf.Clamp(pos.y, -48f, 48f);
            }

            Instantiate(Enemies[rand], pos, Quaternion.identity);
        }

        enemiesOnScreen += enemiesNumber;
    }
}

