using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGolemScript : MonoBehaviour
{
    public float jumpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float horizontalSpeed = 10;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform headCheck;
    private float groundRadius = 0.5f;
    private float headRadius;
    private bool grounded;
    private bool headTouch;
    private float hAxis;
    private bool facingRight;

    private bool jump;

    private Rigidbody2D golemRigidbody;



    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        grounded = false;
        facingRight = true;

        golemRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
        hAxis = Input.GetAxis("Horizontal");

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;

        float yVelocity = golemRigidbody.velocity.y;

        if (grounded)
        {
            if ((hAxis > 0) && (facingRight == false))
            {
                Flip();
            }
            else if ((hAxis < 0) && (facingRight == true))
            {
                Flip();
            }

        }
        if (grounded && !jump)
        {
            golemRigidbody.velocity = new Vector2(horizontalSpeed * hAxis, golemRigidbody.velocity.y);
        }
        else if (grounded && jump)
        {
            golemRigidbody.velocity = new Vector2(golemRigidbody.velocity.x, jumpSpeed);
        }

        if (golemRigidbody.velocity.y < 0)
        {
            golemRigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if ((golemRigidbody.velocity.y > 0) && (!Input.GetKey(KeyCode.Space)))
        {
            golemRigidbody.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {


    }

    private void Flip()
    {

    }

}
