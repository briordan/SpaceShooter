using UnityEngine;
using System.Collections;

public class FireButton : MonoBehaviour {

    void OnEnable()
    {
        EasyButton.On_ButtonUp += On_ButtonUp;
    }
    
    void On_ButtonUp (string buttonName)
    {
        if (buttonName == "FireButton")
        {
            Debug.Log("Fire! ");
        }
    
    }


}
