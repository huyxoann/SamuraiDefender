using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private string saveFilePath = "SaveFile/save.json";
    public Button continueButton;

    private void Awake()
    {

    }

    void Update()
    {
        if (File.Exists(saveFilePath))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void DisableButtonClick()
    {
        continueButton.interactable = false;
    }
}