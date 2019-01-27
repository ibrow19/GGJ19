using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput2 : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;


    //public Button selectButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        EventSystem EVRef = EventSystem.current; // get the current event system
        EVRef.SetSelectedGameObject(this.gameObject);   // set current selected button
                                                        //    Hack: Move up to the button above us (so we can hack a highlight on it)
        Button bref = EVRef.currentSelectedGameObject.GetComponent<Button>();
        EVRef.SetSelectedGameObject(bref.navigation.selectOnUp.gameObject, null);

    }
    


}
