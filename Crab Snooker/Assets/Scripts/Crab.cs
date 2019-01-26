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

    // Constant attributes.
    private const float moveSpeed = 1f;

    // Action axis bindings.
    public string moveXAxis;
    public string moveYAxis;
    public string lookXAxis;
    public string lookYAxis;
    public string attackAxis;
    public string blockAxis;

    //Rotations
    //float rotVal = 0;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    float rotSpeed = 0.1f;
    //Inputs
    private float xValR;
    private float yValR;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        handleMove();
        handleRotate();
    }

    private void handleMove()
    {
        Vector2 move = getMove();
        transform.Translate(move * Time.deltaTime * moveSpeed, Space.World);
    }

    private Vector2 getMove()
    {
        float x = Input.GetAxis(moveXAxis);
        float y = Input.GetAxis(moveYAxis);
        Vector2 move = new Vector2(x, y);
        //move.Normalize();
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

    private void handleRotate()
    {
        // get right stick components
        xValR = Input.GetAxis(lookXAxis);
        yValR = Input.GetAxis(lookYAxis);
        
        // Get angle of stick rotation
        float tiltAroundZ = Mathf.Rad2Deg * Mathf.Atan2(yValR, xValR);

        // Set up quaternion for slerping
        Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);

        // Don't rotate if joystick is in idle position
        if (xValR == 0 && yValR == 0)
            target = transform.rotation;

        // Slerp objects towrds stick direction, smooth determines speed of rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * smooth);
    }
}
