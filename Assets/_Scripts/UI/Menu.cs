using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private bool isPlayingBefTrans;
    //  Sound effect refs
    public  AudioSource clickSound;
    public  AudioSource hoverSound;

    //  Menu refs
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider globalAudioSlider;

    void Start() {
        globalAudioSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        globalAudioSlider.value = 0.5f;
        globalAudioSlider.value = AudioListener.volume;
    }

    //  Used in pause menu

    public void Continue() {
    Time.timeScale = 1;
    settingsMenu.SetActive(false);
    startMenu.SetActive(false);
   }
    public void BackToMenuSceneBtn() {
        SceneManager.LoadScene(0);
    }

    // Used in settings menu
    public void BackBtn() {
    settingsMenu.SetActive(false);
    startMenu.SetActive(true);
   }

   public void ValueChangeCheck() {
    AudioListener.volume = globalAudioSlider.value;
   }

    //  Used in start menu
   public void PlayBtn() {
        StartCoroutine(TransitionCoroutine(1, clickSound.clip.length));
   }
   public void SettingsBtn() {
    startMenu.SetActive(false);
    settingsMenu.SetActive(true);
   }
   public void QuitBtn() {
    Application.Quit();
   }

   //   Button sound effects
   
    
    public void HoverSound() {
        if(!hoverSound.isPlaying)
            hoverSound.Play();
    }
    public void ClickSound() {
        if(!clickSound.isPlaying)
            clickSound.Play();
    }

     IEnumerator TransitionCoroutine(int sceneIndex, float waitForSec)
    {
        yield return new WaitForSeconds(waitForSec);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
