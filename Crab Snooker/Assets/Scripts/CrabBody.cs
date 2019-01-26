using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBody : MonoBehaviour
{
    private Animator animator;
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // TODO: play audio here.


            RegularBall otherBall = collision.gameObject.GetComponent<RegularBall>();

            if (otherBall.isPowered())
            {
                alive = false;
            }
        }
    }

    public bool isAlive()
    {
        return alive;
    }
}
