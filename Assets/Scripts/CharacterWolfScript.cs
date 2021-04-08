using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWolfScript : MonoBehaviour
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
    public GameObject GolemCharacter;
    public GameObject BirdCharacter;

    private bool jump;
    private bool change;

    private Rigidbody2D wolfRigidbody;



    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        grounded = false;
        facingRight = true;

        wolfRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
        change = Input.GetKeyDown(KeyCode.Z);
        hAxis = Input.GetAxis("Horizontal");

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;

        float yVelocity = wolfRigidbody.velocity.y;

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
            wolfRigidbody.velocity = new Vector2(horizontalSpeed * hAxis, wolfRigidbody.velocity.y);
        }
        else if (grounded && jump)
        {
            wolfRigidbody.velocity = new Vector2(wolfRigidbody.velocity.x, jumpSpeed);
        }

        if (wolfRigidbody.velocity.y < 0)
        {
            wolfRigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if ((wolfRigidbody.velocity.y > 0) && (!Input.GetKey(KeyCode.Space)))
        {
            wolfRigidbody.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        if (change && grounded)
        {
            GameObject changeToGolem;
            changeToGolem = Instantiate(GolemCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (change && !grounded)
        {
            GameObject changeToBird;
            changeToBird = Instantiate(BirdCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    void FixedUpdate()
    {


    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 wolfScale = transform.localScale;
        wolfScale.x *= -1;
        transform.localScale = wolfScale;
    }

}
