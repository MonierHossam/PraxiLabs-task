using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// To manage UI update and interaction
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button redButton;
    public Button greenButton;
    public Button blueButton;

    public Text shapeText;

    public GameObject instructionsPanel;

    void Start()
    {
        Instance = this;

        //add listeners to the on click events
        redButton.onClick.AddListener(delegate { ColorButtonClicked(Color.red); });
        greenButton.onClick.AddListener(delegate { ColorButtonClicked(Color.green); });
        blueButton.onClick.AddListener(delegate { ColorButtonClicked(Color.blue); });

        StartCoroutine(CloseInstructions());//start the 5 seconds timer to close the instructions screen
    }

    /// <summary>
    /// Close instructions screen after 5 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseInstructions()
    {
        yield return new WaitForSeconds(5);

        instructionsPanel.SetActive(false);
        InputManager.playing = true;
    }

    /// <summary>
    // Called when onclick event is fired to update the color of the shape
    /// </summary>
    /// <param name="mColor"></param>
    private void ColorButtonClicked(Color mColor)
    {
        ObjectsManager.Instance.UpdateColor(mColor);
    }

    /// <summary>
    /// Update the Shape name in UI 
    /// </summary>
    /// <param name="text"> The new text string </param>
    public void UpdateShapeText(string text)
    {
        shapeText.text = text;
    }

}
