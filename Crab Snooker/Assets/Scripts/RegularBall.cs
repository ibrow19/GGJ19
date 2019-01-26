using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBall : MonoBehaviour
{
    private const float powerSpeed = 5f;

    private float depowerTime = 0f;
    private const float depowerLimit = 0.2f;
    private bool powered = false;


    private ParticleSystem particles;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        rigidBody = GetComponent<Rigidbody2D>();
        particles.enableEmission = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        // depowerTime after set time below power speed.
        if (powered)
        {
            if (rigidBody.velocity.magnitude < powerSpeed)
            {
                depowerTime += Time.deltaTime; 
            }
            else
            {
                depowerTime = 0f;
            }

            if (depowerTime >= depowerLimit)
            {
                Debug.Log(rigidBody.velocity.magnitude);
                Debug.Log("Depowering");
                powered = false;
                depowerTime = 0f;
                particles.enableEmission = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // TODO: play audio here.

            if (powered)
            {
                RegularBall otherBall = collision.gameObject.GetComponent<RegularBall>();
                otherBall.powerUp();
            }
        }
    }

    public bool isPowered()
    {
        return powered; 
    }

    public void powerUp()
    {
        powered = true;
        particles.enableEmission = true;
        depowerTime = 0f;
    }
}
