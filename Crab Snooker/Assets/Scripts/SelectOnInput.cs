using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;

    // boolean for if button selected, defaults to false
    private bool buttonSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if gamepad input detected and button not already selected
        if (Input.GetAxisRaw("VerticalL") != 0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
        print(Input.GetAxisRaw("VerticalL"));
    }

    // When game object deactivated button no longer selected
    private void OnDisable()
    {
        buttonSelected = false;
    }
}
