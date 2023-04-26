using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;

    private Animator anim;

    private float dirX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = rb.GetComponent<SpriteRenderer>();
        anim = rb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2 (dirX * 7f, rb.velocity.y);

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpUp") || Input.GetButtonDown("JumpArrowUp"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 14f);
        }

        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate()
    {
        if (dirX > 0f)
        {
            anim.SetBool("isRunning", true);
            rbSprite.flipX = false;
        }
        else if(dirX < 0f)
        {
            anim.SetBool("isRunning", true);
            rbSprite.flipX = true;
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}
