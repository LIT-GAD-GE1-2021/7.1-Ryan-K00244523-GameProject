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
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask whatIsEnemy;

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
    private bool isMoving;

    private Rigidbody2D wolfRigidbody;
    public Animator wolfAnimator;

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
        // JH - Get the Cinemachine Virtual Camera to follow the
        // transform of this game object
        LevelManagerScript.instance.setVCamFollow(transform);
        isMoving = false;
        jump = false;
        grounded = false;
        waitingChangeToGolem = false;

        LevelManagerScript.instance.currentCharacter = "Wolf";

        wolfRigidbody = GetComponent<Rigidbody2D>();
        wolfAnimator = GetComponent<Animator>();
    }
 
    // Update is called once per frame
    void Update()
    {
        jump = Input.GetKeyDown(KeyCode.X);
        changeToGolem = Input.GetKey(KeyCode.LeftShift);
        changeBird = Input.GetKeyUp(KeyCode.LeftShift);
        hAxis = Input.GetAxis("Horizontal");
        wolfAnimator.SetFloat("walkSpeed" , hAxis * hAxis);


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
            wolfAnimator.SetBool("Jump", true);
          
            wolfRigidbody.velocity = new Vector2(wolfRigidbody.velocity.x, jumpSpeed);
        }
        else if (grounded && !jump)
        {
            wolfAnimator.SetBool("Jump", false);
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
        if (!changeToGolem && grounded)
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
        else if(Input.GetKeyDown(KeyCode.Z)==true)
        {
            Attack();
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyA" || collision.collider.tag == "Spike")
        {
            if (LevelManagerScript.instance.characterCurrentHp > 1)
            {
                TakeDamage(1);
            }
            else
            {
                CharacterDead();
            }
        }
    }
    void Attack()
    {
        FindObjectOfType<LevelManagerScript>().PlayAudio("WolfAttack");
        wolfAnimator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemy);

        foreach( Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAScript>().TakeDamage(1);
        }
    }
    void TakeDamage( int damage )
    {
        FindObjectOfType<LevelManagerScript>().PlayAudio("CharacterHurt");
        LevelManagerScript.instance.EnemyHitPlayer(damage);
        wolfAnimator.SetTrigger("TakeDamage");
    }

    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void CharacterDead()
    {
       
        Debug.Log("characterdead");
        wolfAnimator.SetTrigger("Dead");
        LevelManagerScript.instance.GameOver();
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CharacterWolfScript>().enabled = false;
        this.enabled = false;
    }


}
