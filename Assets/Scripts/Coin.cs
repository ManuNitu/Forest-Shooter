using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed;

    private Transform player;
    private GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }

    private void Start()
    {
        transform.SetParent(null);

        if (parent != null)
            Destroy(parent.gameObject, 0.1f);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CoinsUI.coinsNumber++;

            AudioManager.Instance.PlaySfx(AudioManager.Instance.pickUpCoins);

            Destroy(this.gameObject);
        }    
    }
}
