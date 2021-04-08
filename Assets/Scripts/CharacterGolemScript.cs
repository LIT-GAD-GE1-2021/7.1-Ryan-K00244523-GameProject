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
    public GameObject WolfCharacter;

    private bool jump;
    private bool changeToWolf;

    private Rigidbody2D wolfRigidbody;

    void Awake()
    {
        if (LevelManagerScript.facingRightMainBool == true)
        {
            Vector3 golemScale = transform.localScale;
            golemScale.x = 1;
            transform.localScale = golemScale;
        }
        else
        {
            Vector3 golemScale = transform.localScale;
            golemScale.x = -1;
            transform.localScale = golemScale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        grounded = false;

        wolfRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetKeyDown(KeyCode.X);
        changeToWolf = Input.GetKey(KeyCode.LeftShift);
        hAxis = Input.GetAxis("Horizontal");

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;

        float yVelocity = wolfRigidbody.velocity.y;


            if ((hAxis > 0) && (LevelManagerScript.facingRightMainBool == false))
            {
                Flip();
            }
            else if ((hAxis < 0) && (LevelManagerScript.facingRightMainBool == true))
            {
                Flip();
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

        if (changeToWolf && grounded)
        {
            ChangeToWolf();
        }
    }


    void FixedUpdate()
    {


    }

    private void Flip()
    {
        LevelManagerScript.facingRightMainBool = !LevelManagerScript.facingRightMainBool;
        Vector3 golemScale = transform.localScale;
        golemScale.x *= -1;
        transform.localScale = golemScale;
    }

    private void ChangeToWolf()
    {
        GameObject changeToWolf;
        changeToWolf = Instantiate(WolfCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }

}
