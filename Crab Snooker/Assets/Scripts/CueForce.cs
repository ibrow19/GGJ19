using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueForce : MonoBehaviour
{
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive(bool isActive)
    {
        GetComponent<Collider2D>().enabled = isActive; 
        if (active)
        {
            Debug.Log("Is active");
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
        //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger");
        if (active)
        {
            Debug.Log("Firing");
        }
    }
}
