using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        float minAngle = -5f;
        float maxAngle = 5f;
        float angle = Random.Range(minAngle, maxAngle);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Vector3 dir = rotation * transform.right;
        
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddTorque(Random.Range(-100.0f, 100.0f));
        rb2d.AddForce(dir * Random.Range(800.0f,1000.0f));

    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            // Power up ball on first update.
            GetComponent<RegularBall>().powerUp();
            first = false;
        }
    }
}
