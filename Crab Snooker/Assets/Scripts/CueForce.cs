using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueForce : MonoBehaviour
{
    public float force = 1000f;
    private Vector3 refScale;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive(bool isActive, Vector3 scale)
    {
        refScale = scale;
        GetComponent<Collider2D>().enabled = isActive; 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 dir = transform.rotation * new Vector3(1, 0, 0);

        // Correct direction based on parent scaling.
        if (refScale.x < 0)
        {
            dir *= -1;
        }

        // Apply force.
        collider.attachedRigidbody.AddForce(dir * force);
    }
}
