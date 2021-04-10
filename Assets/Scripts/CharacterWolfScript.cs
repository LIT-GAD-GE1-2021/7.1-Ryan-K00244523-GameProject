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
    private bool changeToGolem;
    private bool changeBird;
    private bool waitingChangeToGolem;

    private Rigidbody2D wolfRigidbody;

    void Awake()
    {
        if (LevelManagerScript.facingRightMainBool == true)
        {
            Vector3 wolfScale = transform.localScale;
            wolfScale.x = wolfScale.x * wolfScale.x / wolfScale.x;
            transform.localScale = wolfScale;
        }
        else
        {
            Vector3 wolfScale = transform.localScale;
            wolfScale.x = -wolfScale.x;
            transform.localScale = wolfScale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        grounded = false;
        waitingChangeToGolem = false;

        wolfRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetKeyDown(KeyCode.X);
        changeToGolem = Input.GetKeyUp(KeyCode.LeftShift);
        changeBird = Input.GetKeyUp(KeyCode.LeftShift);
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

        if (!jump)
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
        if (changeToGolem & !grounded)
        {
            waitingChangeToGolem = true;
        }
        if (changeToGolem && grounded)
        {
            ChangeToGolem();
        }
        else if (jump && !grounded)
        {
            ChangeToBird();
        }
        else if ( waitingChangeToGolem == true && grounded)
        {
            ChangeToGolem();
        }
    }


    void FixedUpdate()
    {


    }

    private void Flip()
    {
        LevelManagerScript.facingRightMainBool = !LevelManagerScript.facingRightMainBool;
        Vector3 wolfScale = transform.localScale;
        wolfScale.x *= -1;
        transform.localScale = wolfScale;
    }

    private void ChangeToGolem()
    {
        GameObject changeToGolem;
        changeToGolem = Instantiate(GolemCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }    

    private void ChangeToBird()
    {
        GameObject changeToBird;
        changeToBird = Instantiate(BirdCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }
 
}
