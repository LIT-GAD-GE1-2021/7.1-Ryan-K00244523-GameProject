using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGolemScript : MonoBehaviour
{
    public float jumpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float horizontalSpeed;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform headCheck;
    private float groundRadius;
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

        if (grounded)
        {
            if ((hAxis>0) && (facingRight == false))
            {
                Flip();
            }
            else if ((hAxis<0) && (facingRight == true))
            {
                Flip();
            }

        }
    }

    void FixedUpdate()
    {
        
    }

    private void Flip()
    {

    }

}
