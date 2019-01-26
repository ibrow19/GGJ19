using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;
    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.StartsWith("Crab"))
        {
            if (col.gameObject.tag.Equals("Protected"))
            {
                //this.gameObject.//rebound like crab was ball
            }
            else
            {
                if (col.gameObject.tag.Equals("Shooting"))
                {
                    //some kind of rebound
                }
                else
                {
                    rb2d.AddForce(transform.right * 10.0f);

                    //other crab wins or hit crab loses
                }
            }
        }
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddTorque(Random.Range(-100.0f, 100.0f));
        rb2d.AddForce(transform.right * Random.Range(500.0f,1000.0f));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
