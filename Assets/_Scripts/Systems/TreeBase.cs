using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBase : MonoBehaviour
{
    public GameObject[] prefabs;
    private GameObject currentPrefab;
    private int currentGrowthIndex = 0;
    private int growthStageCount = 0;
    public GrowthStage currentGrowthStage;

    public int waterUsage;
    public int nitrogenUsage;
    public int lightGain;
    public int co2Gain;
    public enum GrowthStage
    {
        Seed,
        Sprout,
        Sapling,
        YoungTree,
        Mature,
        Elder
    }


    void Start()
    {
        currentGrowthStage = (GrowthStage)currentGrowthIndex;
        growthStageCount = System.Enum.GetValues(typeof(GrowthStage)).Length;
        SetPrefab();
        SetModifiers();
    }


    public int[] GetModifiers()
    {
        int[] integers = { waterUsage, nitrogenUsage, lightGain, co2Gain };
        return integers;
    }

    public void Grow()
    {
        if (currentGrowthIndex < growthStageCount - 1) {
            currentGrowthIndex++;
            currentGrowthStage = (GrowthStage)currentGrowthIndex;
            SetPrefab();
            SetModifiers();
        } else {
            //disable grow btn
            //STOP GROWING
            Debug.Log("You're already at the final growth stage!'");
        }
    }
    
    private void SetModifiers()
    {
        switch(currentGrowthStage)
        {
            case GrowthStage.Seed:
                waterUsage = -5;
                nitrogenUsage = -1;
                lightGain = 0;
                co2Gain = 0;
                break;
            case GrowthStage.Sprout:
                waterUsage = -10;
                nitrogenUsage = -2;
                lightGain = 5;
                co2Gain = 5;
                break;
            case GrowthStage.Sapling:
                waterUsage = -20;
                nitrogenUsage = -4;
                lightGain =10;
                co2Gain = 10;
                break;
            case GrowthStage.YoungTree:
                waterUsage = -40;
                nitrogenUsage = -8;
                lightGain = 20;
                co2Gain = 20;
                break;
            case GrowthStage.Mature:
                waterUsage = -80;
                nitrogenUsage = -16;
                lightGain = 40;
                co2Gain = 40;
                break;
            case GrowthStage.Elder:
                waterUsage = -160;
                nitrogenUsage = -32;
                lightGain = 80;
                co2Gain = 80;
                break;
        }
    }

    private void SetPrefab()
    {
        Destroy(currentPrefab);
        currentPrefab = Instantiate(prefabs[currentGrowthIndex]);
        currentPrefab.transform.position = transform.position;
    }
}
