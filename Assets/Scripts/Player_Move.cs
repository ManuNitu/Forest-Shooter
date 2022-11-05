using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Move : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    private float activeSpeed;

    public float dashLenght;
    public float dashCooldown;
    private float dashCounter;
    private float dashCoolCounter;

    private Rigidbody2D rb;
    private Animator anim;
    Vector2 dirrection;
    Vector2 lockedDirrection;
    private bool facingRight = true;
    private void Awake()
    {
        activeSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        dirrection.x = Input.GetAxisRaw("Horizontal");
        dirrection.y = Input.GetAxisRaw("Vertical");

        if (activeSpeed == dashSpeed && !anim.GetBool("roll"))
            anim.SetBool("roll", true);
        else if(activeSpeed != dashSpeed && anim.GetBool("roll"))
            anim.SetBool("roll", false);

        anim.SetFloat("speed", dirrection.magnitude);

        if(activeSpeed == dashSpeed)
        {
            difference = lockedDirrection;
        }
        if (difference.x > 0 && !facingRight)
            Flip();
        else if (difference.x < 0 && facingRight)
            Flip();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCounter <= 0 && dashCoolCounter <= 0)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Instance.playerDash);

                lockedDirrection = dirrection;
                if (dirrection.magnitude == 0)
                {
                    if (anim.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
                        lockedDirrection = transform.right;
                    else
                        lockedDirrection = -transform.right;
                }

                activeSpeed = dashSpeed;
                dashCounter = dashLenght;
            }
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
            dashCoolCounter -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (activeSpeed == dashSpeed)
            dirrection = lockedDirrection;
        rb.MovePosition(rb.position + dirrection.normalized * activeSpeed * Time.fixedDeltaTime);
    }
    void Flip()
    {
        facingRight = !facingRight;
        if (facingRight)
        {
            anim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
            anim.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
