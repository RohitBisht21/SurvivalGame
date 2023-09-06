using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera menuCam;
    public Canvas menuCanvas;
    public Canvas sliderCanvas;
    public Canvas optionCanvas;
    public OptionMenuController optionMenuController;
   public void PlayGame()
    {
        menuCam.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        sliderCanvas.gameObject.SetActive(true);

        // Call the method to re-enable audio sources in the OptionMenuController
        if (optionMenuController != null)
        {
            optionMenuController.EnableAudioSources();
        }
    }
    public void OptionMenu()
    {
        menuCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        optionCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
