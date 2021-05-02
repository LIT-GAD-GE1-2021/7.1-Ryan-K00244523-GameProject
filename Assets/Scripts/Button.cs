using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator buttonAnimator;

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
            FindObjectOfType<LevelManagerScript>().PlayAudio("DoorOpen");
            buttonAnimator.SetTrigger("ButtonPress");
            doorAnimator.SetTrigger("DoorOpen");
        }
    }
}
