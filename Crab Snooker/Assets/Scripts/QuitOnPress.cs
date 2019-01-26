using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnPress : MonoBehaviour
{
    //Quit when pressed
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
