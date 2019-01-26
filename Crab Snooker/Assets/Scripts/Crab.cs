using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    enum State
    {
        SLEEP,
        BLOCK,
        TRANSITIN,
        TRANSITOUT,
        NEUTRAL,
        SHOOTING,
    }

    // Current state of crab.
    private State state = State.SLEEP;

    // Movement attributes (public for access in editor).
    public float maxSpeed = 1f;
    public float acceleration = 1f;

    // Rotation constant attributes.
    private const float smooth = 5.0f;

    // Timer for current state.
    private float stateTime = 0f;

    // Animation times.
    private const float transitTime = 0.1f;
    private const float blockTime = 1f;
    private const float shootTime = 0.08f;

    // Transformation of the crab body.
    public Transform bodyT;

    private SpriteRenderer legRenderer;

    // Action axis bindings.
    public string moveXAxis;
    public string moveYAxis;
    public string lookXAxis;
    public string lookYAxis;
    public string shootAxis;
    public string blockAxis;

    public CueForce cueForce;

    public Animator bodyAnimator;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        legRenderer = GetComponent<SpriteRenderer>();
        legRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.SLEEP)
        {
            return;
        }

        if ((state == State.TRANSITOUT || state == State.TRANSITIN) &&
             stateTime > transitTime)
        {
            if (state == State.TRANSITIN)
            {
                setState(State.BLOCK);
                bodyAnimator.SetTrigger("shellIn");
            }
            else
            {
                setState(State.NEUTRAL);
                bodyAnimator.SetTrigger("shellOut");
            }
        }
        else if (state == State.BLOCK && stateTime > blockTime)
        {
            setState(State.TRANSITOUT);
            bodyAnimator.SetTrigger("shellOut");
        }
        else if (state == State.SHOOTING && stateTime > shootTime)
        {
            setState(State.NEUTRAL);
            bodyAnimator.SetTrigger("endPoke");
        }
        else if (state == State.NEUTRAL && Input.GetAxisRaw(shootAxis) > 0)
        {
            setState(State.SHOOTING);
            bodyAnimator.SetTrigger("startPoke");
        }
        else if (state == State.NEUTRAL && Input.GetAxisRaw(blockAxis) > 0)
        {
            setState(State.TRANSITIN);
            bodyAnimator.SetTrigger("shellIn");
        }
    }

    void FixedUpdate()
    {
        if (state == State.SLEEP)
        {
            return;
        }

        // Update state timer.
        stateTime += Time.deltaTime;

        if (state == State.NEUTRAL)
        {
            handleRotate();
            handleMove();
        }
    }

    public void activate()
    {
        state = State.TRANSITOUT;         
        bodyAnimator.SetTrigger("shellOut");
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

    private void setState(State newState)
    {
        stateTime = 0f;
        state = newState;
        cueForce.setActive(newState == State.SHOOTING, transform.localScale);
        legRenderer.enabled = (newState == State.SHOOTING || newState == State.NEUTRAL);
    }
}
