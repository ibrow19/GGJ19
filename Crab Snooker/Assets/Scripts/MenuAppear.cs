using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAppear : MonoBehaviour
{
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        Canvas.gameObject.SetActive(true);
    }

    public void deactivate()
    {
        Canvas.gameObject.SetActive(false);
    }

}
