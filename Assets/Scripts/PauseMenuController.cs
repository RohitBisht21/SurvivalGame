using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
	public Canvas pauseCanvas;
	public Canvas menuCanvas;
	public Canvas optionMenuCanvas;
	public Canvas deadCanvas;
	public Camera menuCam;
	public List<AudioSource> audioSources;

	public void Update()
	{
		 if (Input.GetKey(KeyCode.Escape) && !menuCanvas.gameObject.activeSelf && !optionMenuCanvas.gameObject.activeSelf && !deadCanvas.gameObject.activeSelf)
        {
			Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(true);
			 foreach (AudioSource audioSource in audioSources)
			 {
				  audioSource.enabled=false;
			 }
			AudioManager.Instance.SetAllAudioSourcesActive(false);
        }
	}
	public void Resume()
	{
		pauseCanvas.gameObject.SetActive(false);
		Time.timeScale = 1;
		 foreach (AudioSource audioSource in audioSources)
			 {
				  audioSource.enabled=true;
			 }
		AudioManager.Instance.SetAllAudioSourcesActive(true);
	}

	public void Options()
	{
		pauseCanvas.gameObject.SetActive(false);
		optionMenuCanvas.gameObject.SetActive(true);
		Time.timeScale = 0;
		 foreach (AudioSource audioSource in audioSources)
			 {
				  audioSource.enabled=true;
			 }
	}
}
