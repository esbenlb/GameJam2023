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

    public float waterUsage;
    public float nitrogenUsage;
    public float lightGain;
    public float co2Gain;
    public enum GrowthStage
    {
        Seed,
        Sprout,
        Seedling,
        Sapling,
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
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Grow();
        }
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
                waterUsage = 1f;
                nitrogenUsage = 0.2f;
                lightGain = 0f;
                co2Gain = 0f;
                break;
            case GrowthStage.Sprout:
                waterUsage = 2f;
                nitrogenUsage = 0.4f;
                lightGain = 1f;
                co2Gain = 1f;
                break;
            case GrowthStage.Seedling:
                waterUsage = 4f;
                nitrogenUsage = 0.8f;
                lightGain =2f;
                co2Gain = 2f;
                break;
            case GrowthStage.Sapling:
                waterUsage = 8f;
                nitrogenUsage = 1.6f;
                lightGain = 4f;
                co2Gain = 4f;
                break;
            case GrowthStage.Mature:
                waterUsage = 16f;
                nitrogenUsage = 3.2f;
                lightGain = 8f;
                co2Gain = 8f;
                break;
            case GrowthStage.Elder:
                waterUsage = 32f;
                nitrogenUsage = 6.4f;
                lightGain = 16f;
                co2Gain = 16f;
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
