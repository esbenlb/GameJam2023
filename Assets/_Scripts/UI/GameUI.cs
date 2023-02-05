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
    [SerializeField] TextMeshProUGUI resourcesTextVal1;
    [SerializeField] TextMeshProUGUI resourcesTextVal2;
    [SerializeField] TextMeshProUGUI resourcesTextVal3;
    [SerializeField] TextMeshProUGUI eventHeadline;

    //  UI interface objects

    [SerializeField] GameObject eventUI;

    //  treeBase refs

    public TreeBase treeBase;
    public static bool upgradeAvailable;

    //  Sound effect refs
    public  AudioSource clickUpgradeSound;
    public  AudioSource hoverUpgradeSound;

    //  Button refs
    public GameObject growBtn;

    private Color prevWaterCol;
    private Color prevNitroCol;

    void Start() {
        prevWaterCol = resourcesTextVal1.color;
        prevNitroCol = resourcesTextVal2.color;
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        gridSystem = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<GridSystem>();
        eventUI.SetActive(false);
        
        InvokeRepeating("DayNbrHandler",/*start in*/ 0.2f ,/*every*/ master.GetDayLength());
        
        GameObject Tree = GameObject.Find("Tree");
        treeBase = Tree.GetComponent<TreeBase>();
    }

    void Update() {
        updateUIText();
    }

    private void updateUIText() {
        resourcesText.text = 
        "Light\n"+
        "Water\n"+
        "Nitrogen\n"
        ;
        resourcesTextVal1.text =
        master.stats.light.ToString("D2")
        ;
        resourcesTextVal2.text =
        "\n" +
        master.stats.water.ToString("D2")
        ;
        resourcesTextVal3.text =
        "\n" +
        "\n" +
        master.stats.nitrogen.ToString("D2")
        ;
        eventHeadline.text =
        "Day " +
        dayNbr.ToString()
        ;
        if(master.stats.water <= 5f)
            resourcesTextVal1.color = new Color(1,0,0,1);
        else if(master.stats.water > 5f)
            resourcesTextVal1.color = prevNitroCol;
        if(master.stats.nitrogen <= 5f)
            resourcesTextVal2.color = new Color(1,0,0,1);
        else if(master.stats.nitrogen > 5f)
            resourcesTextVal2.color = prevNitroCol;

    }

    public void Upgrade() {
        treeBase.Grow();
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
            Destroy(master.roots[i]);
        }
        master.roots = new();
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
        StartCoroutine(CoolDownCoroutine(0.0f));
    }
}