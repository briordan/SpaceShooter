using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    #region Fields
    public Texture backgroundTexture;

    private string instructionText = "Instructions\nPress Left and Right Arrows to move.\nPress Space Bar to Fire\n";
    private int buttonWidth = 200;
    private int buttonHeight = 50;

    #endregion

    #region Properties
    #endregion

    #region Functions
    void Start()
    {

    }


    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
        GUI.Label(new Rect(10, 10, 250, 200), instructionText);
        
        if (Input.anyKeyDown)
            Application.LoadLevel(1); // start game
        
       
    }
    #endregion
}
