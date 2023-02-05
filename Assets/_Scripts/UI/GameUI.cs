using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    static public int dayNbr = 0;
    private Master master;
    private GridSystem gridSystem;

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
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        gridSystem = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<GridSystem>();
        eventUI.SetActive(false);
        
        InvokeRepeating("DayNbrHandler",/*start in*/ 0.2f ,/*every*/ master.dayLength);
        
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

    void NewSeason(bool spawnRocks = false)
    {
        gridSystem.GenerateMap(spawnRocks);
        for (int i = 0; i < master.roots.Count; i++)
        {
            master.roots.Remove(master.roots[i]);
        }
    }

    void DayNbrHandler()
    {
        eventUI.SetActive(true);
        dayNbr += 1;

        
        if(dayNbr == 1)
        {
            NewSeason(true);
            master.SeasonSpring();

        }
        if(dayNbr == 91)
        {
            NewSeason();
            master.SeasonSummer();
        }
        //Season
        if (dayNbr == 182)
        {
            NewSeason();
            master.Seasonfall();
        }
        //Season
        if (dayNbr == 273)
        {
            NewSeason(true);
            master.SeasonWinter();
        }
        //Season
        if (dayNbr == 365)
        {
            // end game
        }


        //  How many seconds before the eventUI is disabled
        StartCoroutine(CoolDownCoroutine(2));
    }
}