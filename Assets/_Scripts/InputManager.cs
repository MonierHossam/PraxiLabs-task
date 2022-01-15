using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detect user input and trigger fucntions based on it
/// </summary>
public class InputManager : MonoBehaviour
{
    public Vector2 mouseInput;

    public static bool playing;

    /// <summary>
    /// Keep checking for user input 
    /// </summary>
    void Update()
    {
        if(playing)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ObjectsManager.Instance.NextShape();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ObjectsManager.Instance.PreviousShape();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ObjectsManager.Instance.UpdateColor(Color.red);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                ObjectsManager.Instance.UpdateColor(Color.green);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                ObjectsManager.Instance.UpdateColor(Color.blue);
            }

            if (Input.GetMouseButton(1))//Get mouse axies to rotate the shape based on its values
            {
                mouseInput.x += Input.GetAxis("Mouse X");
                mouseInput.y += Input.GetAxis("Mouse Y");
                ObjectsManager.Instance.RotateShape(mouseInput);
            }
        }
    }
}
