using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    static public int dayNbr;
    private Master master;

    //  values to display and text field refs

    [SerializeField] TextMeshProUGUI resourcesText;
    [SerializeField] TextMeshProUGUI resourcesTextVals;
    [SerializeField] TextMeshProUGUI eventHeadline;

    //  UI interface objects

    [SerializeField] GameObject eventUI;

    //  treeBase refs

    public TreeBase treeBaseClass;
    public static bool upgradeAvailable;

    //  Sound effect refs
    public  AudioSource clickUpgradeSound;
    public  AudioSource hoverUpgradeSound;

    //  Button refs
    public GameObject growBtn;

    void Start() {
        eventUI.SetActive(false);
        InvokeRepeating("DayNbrHandler",/*start in*/ 2.0f ,/*every*/ 10f);
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        GameObject Tree = GameObject.Find("Tree");
        TreeBase treeBase = Tree.GetComponent<TreeBase>();
    }

    void Update() {
        updateUIText();
    }

    private void updateUIText() {
        resourcesText.text = 
        "CO2\n"+
        "Light\n"+
        "Water\n"+
        "Nitrogen\n"
        ;
        resourcesTextVals.text =
        master.stats.co2.ToString("D2") + "\n" +
        master.stats.light.ToString("D2") + "\n" +
        master.stats.water.ToString("D2") + "\n" +
        master.stats.nitrogen.ToString("D2") + "\n"
        ;
        eventHeadline.text =
        "Day " +
        dayNbr.ToString()
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

     IEnumerator CoolDownCoroutine(float waitForSec)
    {
        yield return new WaitForSeconds(waitForSec);
        if(eventUI.activeSelf)
            eventUI.SetActive(false);
    }

    void DayNbrHandler()
    {
        eventUI.SetActive(true);
        dayNbr += 1;
        /*
        if(dayNbr >= 75)
        //Season
        if(dayNbr >= 150)
        //Season
        if(dayNbr >= 225)
        //Season
        if(dayNbr >= 300)
        //Season
        */

        //  How many seconds before the eventUI is disabled
        StartCoroutine(CoolDownCoroutine(2));
    }
}