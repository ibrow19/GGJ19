using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //inputs
    private float xValL;
    private float yValL;
    private float xValR;
    private float yValR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xValR = Input.GetAxis("HorizontalR");
        yValR = Input.GetAxis("VerticalR");

        print("X dir Left: \n" + xValR);
        //print("Y dir Left: \n" + yValR);
    }
}
