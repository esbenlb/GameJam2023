using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private bool isPlayingBefTrans;
    
    //  Sound effect refs
    public  AudioSource clickUpgradeSound;
    public  AudioSource hoverUpgradeSound;

    //  Button refs
    public Button growBtn;

    //  resource refs
    public int resource;
    public int upgradeCost;

    void Start() {
        growBtn.enabled = false;
    }

    void Update() {
        if(resource >= upgradeCost && !growBtn.enabled)
            growBtn.enabled = true;
        else
            growBtn.enabled = false;
    }

    public void Upgrade() {
        if(resource >= upgradeCost) {
            resource -= upgradeCost;
            //  Upgrade *= 2;
        }
    }

    public void HoverSound() {
        if(!hoverUpgradeSound.isPlaying)
            hoverUpgradeSound.Play();
    }
    public void ClickSound() {
        if(!clickUpgradeSound.isPlaying)
            clickUpgradeSound.Play();
    }

     IEnumerator coolDownCoroutine(int sceneIndex, float waitForSec)
    {
        yield return new WaitForSeconds(waitForSec);
        Time.timeScale = 1;
    }
}
