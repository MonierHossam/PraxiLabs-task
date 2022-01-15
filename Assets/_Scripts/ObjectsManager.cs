using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To manage the objects, its shapes and colors 
/// </summary>
public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance;//To access it as Singleton 

    public Color currentColor = Color.red;
    public GameObject currentObject;

    public int currentIndex = 0;

    public float RotationSpeed = 2;

    public GameObject cube;
    public GameObject sphere;
    public GameObject Cylinder;

    Renderer mRenderer;

    void Start()
    {
        Instance = this;

        currentObject = cube; // to solve null ref on code start

        currentIndex = DataManager.instance.mData.shapeIndex; //Get the saved last shaped user slected

        currentColor = DataManager.instance.mData.selectedColor.GetColor(); // Get the saved last shaped color selected

        ChangeShape();
    }

    /// <summary>
    /// Change current shape with the newly selected color
    /// </summary>
    /// <param name="mColor"> New color </param>
    public void UpdateColor(Color mColor)
    {
        DataManager.instance.mData.selectedColor.SetColor(mColor);
        DataManager.instance.SaveLocal();
        currentColor = mColor;
        mRenderer.material.SetColor("_Color", currentColor);
    }

    /// <summary>
    /// Change Object shape to next one 0,1,2 Cube, Sphere, Cylinder respectively 
    /// </summary>
    public void NextShape()
    {
        currentIndex = (currentIndex + 1) % 3;
        ChangeShape();
    }

    /// <summary>
    /// Change Object shape to previous one 0,1,2 Cube, Sphere, Cylinder respectively 
    /// </summary>
    public void PreviousShape()
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = 2;
        }

        ChangeShape();
    }

    /// <summary>
    /// Update the object shape based on the selected index and updated it with selected color and rotation
    /// </summary>
    public void ChangeShape()
    {
        if (currentIndex == 0)
        {
            currentObject.SetActive(false);
            currentObject = cube;
        }
        else if (currentIndex == 1)
        {
            currentObject.SetActive(false);
            currentObject = sphere;
        }
        else if (currentIndex == 2)
        {
            currentObject.SetActive(false);
            currentObject = Cylinder;
        }

        DataManager.instance.mData.shapeIndex = currentIndex;
        mRenderer = currentObject.GetComponent<Renderer>();
        mRenderer.material.SetColor("_Color", currentColor);
        currentObject.SetActive(true);
        UIManager.Instance.UpdateShapeText(currentObject.name);
        currentObject.transform.localRotation = Quaternion.Euler(DataManager.instance.mData.rotationX,
        DataManager.instance.mData.rotationY, 0);// Last saved rotation

        DataManager.instance.SaveLocal();
    }

    /// <summary>
    /// Rotate object with new mouse values
    /// </summary>
    /// <param name="mouseInput"></param>
    public void RotateShape(Vector2 mouseInput)
    {
        currentObject.transform.localRotation = Quaternion.Euler(mouseInput.x * RotationSpeed, mouseInput.y * RotationSpeed, 0);

        DataManager.instance.mData.rotationX = mouseInput.x * RotationSpeed;
        DataManager.instance.mData.rotationY = mouseInput.y * RotationSpeed;

        DataManager.instance.SaveLocal();
    }

    [ContextMenu("Reset transform")]// allowing resting rotation value from editor for testing 
    
    public void ResetTransform()
    {
        currentObject.transform.localRotation = Quaternion.Euler(0,0, 0);
        DataManager.instance.mData.rotationX = 0;
        DataManager.instance.mData.rotationY =0;

        DataManager.instance.SaveLocal();
    }

}
