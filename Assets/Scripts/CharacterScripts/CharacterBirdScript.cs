using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBirdScript : MonoBehaviour
{
    public float jumpSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float horizontalSpeed = 10;
    public float jumpingMaximum;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform headCheck;
    private float groundRadius = 0.5f;
    private float headRadius;
    private bool grounded;
    private bool headTouch;
    private float hAxis;
    private bool facingRight;
    private float jumpingCount = 0;
    public GameObject GolemCharacter;
    public GameObject WolfCharacter;

    private bool jump;
    private bool change;
    private bool changeToWolf;

    private Rigidbody2D birdRigidbody;
    private Animator birdAnimator;

    void Awake()
    {

        jumpingMaximum = jumpingMaximum - 1;
        if (LevelManagerScript.facingRightMainBool == true)
        {
            Vector3 birdScale = transform.localScale;
            birdScale.x = birdScale.x * birdScale.x / birdScale.x;
            transform.localScale = birdScale;
        }
        else
        {
            Vector3 birdScale = transform.localScale;
            birdScale.x = -birdScale.x;
            transform.localScale = birdScale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // JH - Get the Cinemachine Virtual Camera to follow the
        // transform of this game object
        LevelManagerScript.instance.setVCamFollow(transform);

        jump = true;
        grounded = false;
        LevelManagerScript.instance.currentCharacter = "Bird";
    

        birdRigidbody = GetComponent<Rigidbody2D>();
        birdAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManagerScript.instance.characterCurrentHp <= 0)
        {
            CharacterDead();
        }
        changeToWolf = Input.GetKey(KeyCode.LeftShift);

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;

        float yVelocity = birdRigidbody.velocity.y;

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
            birdRigidbody.velocity = new Vector2(horizontalSpeed * hAxis, birdRigidbody.velocity.y);
        }
        else if (jump &&( jumpingCount <= jumpingMaximum))
        {
            birdAnimator.SetTrigger("FlapWing");
            FindObjectOfType<LevelManagerScript>().PlayAudio("BirdFlap");
            birdRigidbody.velocity = new Vector2(birdRigidbody.velocity.x, jumpSpeed);
            jumpingCount += 1;
        }

        if (birdRigidbody.velocity.y < 0)
        {
            birdRigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if ((birdRigidbody.velocity.y > 0) && (!Input.GetKey(KeyCode.Space)))
        {
            birdRigidbody.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }


     


        if (grounded)
        {
            if (changeToWolf)
            {
                ChangeToWolf();
            }
            else
            {
                ChangeToGolem();
            }
        }

        jump = Input.GetKeyDown(KeyCode.X);
        hAxis = Input.GetAxis("Horizontal");
        
    }


    void FixedUpdate()
    {


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyA" || collision.collider.tag == "Spike")
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        FindObjectOfType<LevelManagerScript>().PlayAudio("CharacterHurt");
        birdAnimator.SetTrigger("TakeDamage");
        LevelManagerScript.instance.EnemyHitPlayer(damage);
        
    }

    private void Flip()
    {
        LevelManagerScript.facingRightMainBool = !LevelManagerScript.facingRightMainBool;
        Vector3 birdScale = transform.localScale;
        birdScale.x *= -1;
        transform.localScale = birdScale;
    }

    private void ChangeToWolf()
    {
        GameObject changeToWolf;
        changeToWolf = Instantiate(WolfCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void ChangeToGolem()
    {
        GameObject changeToGolem;
        changeToGolem = Instantiate(GolemCharacter, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void CharacterDead()
    {

        Debug.Log("characterdead");
        LevelManagerScript.instance.setVCamFollow(null);
        birdAnimator.SetTrigger("Dead");
        LevelManagerScript.instance.GameOver();
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CharacterBirdScript>().enabled = false;
        this.enabled = false;
    }
}
