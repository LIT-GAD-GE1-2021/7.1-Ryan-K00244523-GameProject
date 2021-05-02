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
    public GameObject WolfCharacter;

    private bool jump;
    private bool changeToWolf;

    private Rigidbody2D golemRigidbody;
    private Animator golemAnimator;

    void Awake()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            ChangeToWolf();
        }
        if (LevelManagerScript.facingRightMainBool == true)
        {
            Vector3 golemScale = transform.localScale;
            golemScale.x = golemScale.x * golemScale.x / golemScale.x;
            transform.localScale = golemScale;
        }
        else
        {
            Vector3 golemScale = transform.localScale;
            golemScale.x = -golemScale.x;
            transform.localScale = golemScale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // JH - Get the Cinemachine Virtual Camera to follow the
        // transform of this game object
        LevelManagerScript.instance.setVCamFollow(transform);
        jump = false;
        grounded = false;

        LevelManagerScript.instance.currentCharacter = "Golem";

        golemRigidbody = GetComponent<Rigidbody2D>();
        golemAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetKeyDown(KeyCode.X);
        changeToWolf = Input.GetKey(KeyCode.LeftShift);
        hAxis = Input.GetAxis("Horizontal");

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;

        float yVelocity = golemRigidbody.velocity.y;



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

        if (changeToWolf && grounded)
        {
            //StartCoroutine("waitForSecond");
            ChangeToWolf();
        }
    }


    void FixedUpdate()
    {


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyA" || collision.collider.tag == "Spike")
        {
            FindObjectOfType<LevelManagerScript>().PlayAudio("HitGolem");
        }
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

    public void HitByEnemy()
    {
        Debug.Log("HitPlayer");
        
    }

    IEnumerator waitForSecond()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(0.5f);
        ChangeToWolf();
    }

}
