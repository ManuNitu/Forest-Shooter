using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponentInChildren<SpriteRenderer>())
            return;
        SpriteRenderer objSprite = collision.GetComponentInChildren<SpriteRenderer>();
        if (collision.transform.position.y > transform.position.y)
        {
            if (objSprite.sortingOrder > sprite.sortingOrder)
                sprite.sortingOrder = objSprite.sortingOrder + 3;
        }
        else if (collision.transform.position.y < transform.position.y)
        {
            if (objSprite.sortingOrder < sprite.sortingOrder)
                sprite.sortingOrder = objSprite.sortingOrder - 1;
        }
    }
}
