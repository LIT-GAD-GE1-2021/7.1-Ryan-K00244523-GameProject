using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject summonGameObject;
    public bool createObject;
    // Start is called before the first frame update
    void Start()
    {
        createObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( createObject == true)
        {
            GameObject newTutorial;
            newTutorial = Instantiate(summonGameObject, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            createObject = false;
            Destroy(this.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == true)
        {
            createObject = true;

        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
            createObject = true;
    }

}
