using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    enum State
    {
        NEUTRAL,
    }

    // Current state of crab.
    private State state = State.NEUTRAL;

    // Movement attributes (public for access in editor).
    public float maxSpeed = 1f;
    public float acceleration = 1f;

    // Rotation constant attributes.
    private const float smooth = 5.0f;

    // Transformation of the crab body.
    public Transform bodyT;

    // Action axis bindings.
    public string moveXAxis;
    public string moveYAxis;
    public string lookXAxis;
    public string lookYAxis;
    public string attackAxis;
    public string blockAxis;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        print(Input.GetAxisRaw("X"));
        print(Input.GetAxisRaw("Circle"));
        handleRotate();
        handleMove();
    }

    private void handleMove()
    {
        Vector2 move = getMove() * Time.deltaTime * acceleration;
        Vector2 newVelocity = rigidBody.velocity + move;
        if (newVelocity.magnitude > maxSpeed)
        {
            newVelocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
        }
        rigidBody.velocity = newVelocity;
        //Vector3 destination = transform.position + move * Time.deltaTime * moveSpeed;
        //rigidBody.MovePosition(destination);
        //transform.Translate(move * Time.deltaTime * moveSpeed, Space.World);
    }

    private void handleRotate()
    {
        Vector2 lookDir = getLookDir();
        Vector3 curScale = transform.localScale;

        // Don't rotate if joystick is in idle position
        if (lookDir.magnitude == 0)
        {
            return;
        }
        // Flip when facing left.
        else if ((lookDir.x >= 0 && curScale.x <= 0) ||
                 (lookDir.x < 0 && curScale.x > 0))
        {
            curScale.x *= -1;
            transform.localScale = curScale;
        }

        // Negate target x and y if looking left.
        if (curScale.x < 0)
        {
            lookDir *= -1;
        }
        
        // Get angle of stick rotation
        float tiltAroundZ = Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x);

        // Set up quaternion for slerping
        Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);

        // Slerp objects towrds stick direction, smooth determines speed of rotation
        bodyT.transform.rotation = Quaternion.Slerp(bodyT.transform.rotation, target, Time.deltaTime * smooth);
        bodyT.transform.rotation = Quaternion.RotateTowards(bodyT.transform.rotation, target, Time.deltaTime * smooth);
    }

    private Vector2 getMove()
    {
        float x = Input.GetAxis(moveXAxis);
        float y = Input.GetAxis(moveYAxis);
        Vector2 move = new Vector2(x, y);
        move.Normalize();
        return move;
    }

    private Vector2 getLookDir()
    {
        float x = Input.GetAxis(lookXAxis);
        float y = Input.GetAxis(lookYAxis);
        Vector2 lookDir = new Vector2(x, y);
        lookDir.Normalize();
        return lookDir;
    }

}
