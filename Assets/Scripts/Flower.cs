using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Animator flowerAnimator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<LevelManagerScript>().PlayAudio("PickUp");
            flowerAnimator.SetTrigger("Collected");
            LevelManagerScript.instance.AddScore(300);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Flower>().enabled = false;
            this.enabled = false;
        }
        
    }
}
