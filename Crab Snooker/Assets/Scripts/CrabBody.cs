using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBody : MonoBehaviour
{
    private Animator animator;
    private bool alive = true;
    private bool blocking = true;

    public AudioClip crabHitSound;
    public AudioClip blockHitSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // Play sounds.
            if (blocking)
            {
                audioSource.PlayOneShot(blockHitSound, 1);
            }
            else
            {
                audioSource.PlayOneShot(crabHitSound, 1);
            }

            RegularBall otherBall = collision.gameObject.GetComponent<RegularBall>();

            if (!blocking && otherBall.isPowered())
            {
                alive = false;
            }
        }
    }

    public void setBlocking(bool isBlocking)
    {
        blocking = isBlocking;
    }

    public bool isBlocking()
    {
        return blocking;
    }

    public bool isAlive()
    {
        return alive;
    }
}
