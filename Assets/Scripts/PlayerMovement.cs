using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // variables camel case - private keeps var in this code
    private Rigidbody2D rb;
    private BoxCollider2D terr;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    // use float to allow for joystick support
    // f after int is best practice for float
    private float dirX = 0f;
    // SerializeField allows manipulation through Unity
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // enum to hold any state for our player and access index by Animator
    private enum MovementState { idle, running, jumping, falling }    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        terr = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // GetAxis reduces gradually / GetAxisRaw gets 0 immediately / removes floatiness from character
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // gets velocity from frame before to keep movements fluid 

        if (Input.GetButtonDown("Jump") && NoDoubleJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateFrogAnimation();
    }

    private void UpdateFrogAnimation()
    {
        MovementState state;

        // sets horizontal state movemnet animation
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // sets verticle state movement animation
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        // sets state of anim which is Animator with above information / (int) changes state enum into its index integer
        anim.SetInteger("state", (int)state);
    }

    private bool NoDoubleJump()
    {
        // BoxCast matches size of players box collider / first two arguments
        // 0f = no rotation
        // Vector2.down, .1f moves boxcast down to connect with terrain
        // jumpableGround checks if we overlap with terrain, if so, jump again
        return Physics2D.BoxCast(terr.bounds.center, terr.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}