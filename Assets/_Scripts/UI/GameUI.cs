using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    //  values to display and text field refs

    [SerializeField] public float [] values;
    [SerializeField] TextMeshProUGUI resourcesText;

    //  treeBase refs

    public TreeBase treeBaseClass;
    public static bool upgradeAvailable;

    //  Sound effect refs
    public  AudioSource clickUpgradeSound;
    public  AudioSource hoverUpgradeSound;

    //  Button refs
    public GameObject growBtn;

    //  resource refs
    public int resource;
    public int upgradeCost;

    void Update() {
        updateUIText();
    }

    private void updateUIText() {
        resourcesText.text = 
        "Water    - " + Mathf.FloorToInt(values[0]).ToString() + "\n" +
        "Nitrogen - " + Mathf.FloorToInt(values[1]).ToString() + "\n" +
        "Light    - " + Mathf.FloorToInt(values[2]).ToString() + "\n" +
        "CO2      - " + Mathf.FloorToInt(values[3]).ToString() + "\n"
        ;
    }

    public void Upgrade() {
        if(upgradeAvailable)
            treeBaseClass.Grow();
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