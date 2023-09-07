using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera menuCam;
    public Canvas menuCanvas;
    public Canvas sliderCanvas;
    public Canvas optionCanvas;
    public OptionMenuController optionMenuController;

    public void Start()
    {
       AudioManager.Instance.SetAllAudioSourcesActive(false);
    }

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
         // reset some slider values
         Survival.Instance.ResetSliderValues();

         AudioManager.Instance.SetAllAudioSourcesActive(true);
    }
    public void OptionMenu()  
    {
        menuCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        if(Time.timeScale==1)
        {
        optionCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
        }
        else
        {
            optionCanvas.gameObject.SetActive(false);
            Time.timeScale=1;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
