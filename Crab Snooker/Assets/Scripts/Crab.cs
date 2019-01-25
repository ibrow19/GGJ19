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

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        handleMove(); 
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

    private Vector2 getLookDir(string xAxis, string yAxis)
    {
        float x = Input.GetAxis(xAxis);
        float y = Input.GetAxis(yAxis);
        Vector2 lookDir = new Vector2(x, y);
        lookDir.Normalize();
        return lookDir;
    }
}
