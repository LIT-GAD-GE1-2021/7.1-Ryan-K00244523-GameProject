using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAScript : MonoBehaviour
{
    public float enemySpeed;
    public float enemyFlipSecond;
    public float enemyStaySecond;
<<<<<<< Updated upstream
=======

    public int maxHp = 3;
    private int currentHp;
>>>>>>> Stashed changes
    private bool enemyFlipBool;
    private bool enemyAMoveBool;
    private bool readyToDash;
    private Rigidbody2D enemyARigidbody;
  
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
=======
        currentHp = maxHp;
>>>>>>> Stashed changes
        enemySpeed = -enemySpeed;
        enemyAMoveBool = true;
        enemyFlipBool = true;
        readyToDash = false;
        enemyARigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (enemyAMoveBool == true && readyToDash == false)
=======
        if (enemyAMoveBool == true && readyToDash == false )
>>>>>>> Stashed changes
        {
                enemyARigidbody.velocity = new Vector2(enemySpeed, enemyARigidbody.velocity.y);
        }
        else
        {
            enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x, enemyARigidbody.velocity.y);
        }
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        if (enemyFlipBool == true && readyToDash == false)
=======
        if (enemyFlipBool == true && readyToDash == false )
>>>>>>> Stashed changes
        {  
            StartCoroutine("EnemyMoving");
            enemyFlipBool = false;
        }
    }


<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    void Flip()
    {

            Vector3 enemyScale = transform.localScale;
            enemyScale.x = -enemyScale.x;
            transform.localScale = enemyScale;
            enemySpeed = -enemySpeed;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< Updated upstream
        if ( collision.tag == "Player")
        {
            StartCoroutine("ReadyToDash");
=======
        if ( collision.tag == "Player" )
        {
            StartCoroutine("ReadyToDash");
            
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (transform.localScale.x <= 0)
            {
                enemyARigidbody.velocity = new Vector2(-5, enemyARigidbody.velocity.y);
            }
            else
            {
                enemyARigidbody.velocity = new Vector2(5, enemyARigidbody.velocity.y);
            }
>>>>>>> Stashed changes
        }
    }
    IEnumerator EnemyMoving()
    {
        if (readyToDash == false)
        {
            enemyAMoveBool = true;
<<<<<<< Updated upstream
            yield return new WaitForSeconds(enemyFlipSecond);
            enemyAMoveBool = false;

            yield return new WaitForSeconds(enemyStaySecond);
=======

            yield return new WaitForSeconds(enemyFlipSecond);

            enemyAMoveBool = false;

            yield return new WaitForSeconds(enemyStaySecond);

>>>>>>> Stashed changes
            Flip();
            enemyFlipBool = true;
        }
     

    }

    IEnumerator ReadyToDash()
    {
<<<<<<< Updated upstream
        readyToDash = true;
        enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x, enemyARigidbody.velocity.y);

        yield return new WaitForSeconds(1);

=======
        StopCoroutine("EnemyMoving");
        readyToDash = true;       
        enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x, enemyARigidbody.velocity.y);
        GetComponent<BoxCollider2D>().enabled = false;
        //back for a few range
        if (transform.localScale.x <= 0)
        {
            enemyARigidbody.velocity = new Vector2(-5, enemyARigidbody.velocity.y);
        }
        else
        {
            enemyARigidbody.velocity = new Vector2(5, enemyARigidbody.velocity.y);
        }

        yield return new WaitForSeconds(1.5f);

        //dash
>>>>>>> Stashed changes
        if (transform.localScale.x <= 0)
        {
            enemyARigidbody.velocity = new Vector2(30, enemyARigidbody.velocity.y);
        }
        else
        {
            enemyARigidbody.velocity = new Vector2(-30, enemyARigidbody.velocity.y);
        }
<<<<<<< Updated upstream
          yield return new WaitForSeconds(1);
        enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x, enemyARigidbody.velocity.y);
          yield return new WaitForSeconds(1);
        readyToDash = false;



=======

          yield return new WaitForSeconds(0.5f);

        //forcestop if didnt hit anything and going too far

        enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x/5, enemyARigidbody.velocity.y/5);

          yield return new WaitForSeconds(1);

        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine("EnemyMoving");
        readyToDash = false;
       
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        LevelManagerScript.instance.AddScore(200);
        GetComponent<Animator>().SetTrigger("Dead");
        Debug.Log("EnemyDeath");
        StopAllCoroutines();
        enemyARigidbody.velocity = new Vector2(0,0);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        this.enabled = false;
>>>>>>> Stashed changes
    }


}
