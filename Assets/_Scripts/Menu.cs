using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //  Menu refs
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider globalAudioSlider;

    void Start() {
        globalAudioSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        globalAudioSlider.value = 0.5f;
        globalAudioSlider.value = AudioListener.volume;
    }

    // Used in settings menu and pause menu
    public void BackBtn() {
    settingsMenu.SetActive(false);
    startMenu.SetActive(true);
   }

   public void ValueChangeCheck() {
    AudioListener.volume = globalAudioSlider.value;
    Debug.Log(AudioListener.volume);
   }

    //  Used in start menu
   public void PlayBtn() {
    Debug.Log("Game scene path not set");
    //  SceneManager.LoadScene(1);
   }
   public void SettingsBtn() {
    startMenu.SetActive(false);
    settingsMenu.SetActive(true);
   }
   public void QuitBtn() {
    Application.Quit();
   }
}
