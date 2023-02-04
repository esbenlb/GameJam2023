using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    private Master master;

    //  values to display and text field refs

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

    void Start() {
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        GameObject Tree = GameObject.Find("Tree");
        TreeBase treeBase = Tree.GetComponent<TreeBase>();
    }

    void Update() {
        updateUIText();
    }

    private void updateUIText() {
        resourcesText.text = 
        "Light    " + master.stats.light.ToString() + "\n" +
        "Water    " + master.stats.water.ToString() + "\n" +
        "Nitrogen " + master.stats.nitrogen.ToString() + "\n"
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