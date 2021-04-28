using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAScript : MonoBehaviour
{
    public float enemySpeed;
    public float enemyFlipSecond;
    public float enemyStaySecond;
    private bool enemyFlipBool;
    private bool enemyAMoveBool;
    private bool readyToDash;
    private Rigidbody2D enemyARigidbody;
  
    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = -enemySpeed;
        enemyAMoveBool = true;
        enemyFlipBool = true;
        readyToDash = false;
        enemyARigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAMoveBool == true && readyToDash == false)
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
        if (enemyFlipBool == true && readyToDash == false)
        {  
            StartCoroutine("EnemyMoving");
            enemyFlipBool = false;
        }
    }


    void Flip()
    {

            Vector3 enemyScale = transform.localScale;
            enemyScale.x = -enemyScale.x;
            transform.localScale = enemyScale;
            enemySpeed = -enemySpeed;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player")
        {
            StartCoroutine("ReadyToDash");
        }
    }
    IEnumerator EnemyMoving()
    {
        if (readyToDash == false)
        {
            enemyAMoveBool = true;
            yield return new WaitForSeconds(enemyFlipSecond);
            enemyAMoveBool = false;

            yield return new WaitForSeconds(enemyStaySecond);
            Flip();
            enemyFlipBool = true;
        }
     

    }

    IEnumerator ReadyToDash()
    {
        readyToDash = true;
        enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x, enemyARigidbody.velocity.y);

        yield return new WaitForSeconds(1);

        if (transform.localScale.x <= 0)
        {
            enemyARigidbody.velocity = new Vector2(30, enemyARigidbody.velocity.y);
        }
        else
        {
            enemyARigidbody.velocity = new Vector2(-30, enemyARigidbody.velocity.y);
        }
          yield return new WaitForSeconds(1);
        enemyARigidbody.velocity = new Vector2(enemyARigidbody.velocity.x, enemyARigidbody.velocity.y);
          yield return new WaitForSeconds(1);
        readyToDash = false;



    }


}
