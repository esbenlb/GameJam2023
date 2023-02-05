using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBase : MonoBehaviour
{
    public GameObject[] prefabs;
    public Sprite[] sprites; 
    private GameObject currentPrefab;
    private int currentGrowthIndex = 0;
    private int growthStageCount = 0;
    public GrowthStage currentGrowthStage;
    public List<Vector3> positions = new List<Vector3>();
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
        positions.Add(new Vector3(0,-0.5f,0));
        positions.Add(new Vector3(0,0,0));
        positions.Add(new Vector3(0,0.8f,-1f));
        positions.Add(new Vector3(0,1.5f,0));
        positions.Add(new Vector3(0,2.5f,0));
        positions.Add(new Vector3(0,3f,0));

        currentGrowthStage = (GrowthStage)currentGrowthIndex;
        growthStageCount = System.Enum.GetValues(typeof(GrowthStage)).Length;
        SetPrefab();
        SetModifiers();
    }

    public void ChangeSprite(int seasonIndex)
    {
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //sapling
        if (currentGrowthIndex == 2 && (seasonIndex == 0 || seasonIndex == 1))
        {
            //spring/summer sprite
           spriteRenderer.sprite = sprites[0];
        }
        if (currentGrowthIndex == 2 && seasonIndex == 2)
        {
           //fall sprite 
           spriteRenderer.sprite = sprites[1];
        }
        if (currentGrowthIndex == 2 && seasonIndex == 3)
        {
           //winter sprite 
           spriteRenderer.sprite = sprites[2];
        }

        //young
        if (currentGrowthIndex == 3 && (seasonIndex == 0 || seasonIndex == 1))
        {
           //spring,summer sprite
           spriteRenderer.sprite = sprites[5];
        }
        if (currentGrowthIndex == 3 && seasonIndex == 2)
        {
           //fall sprite
           spriteRenderer.sprite = sprites[4];
        }
        if (currentGrowthIndex == 3 && seasonIndex == 3)
        {
           //winter sprite 
           spriteRenderer.sprite = sprites[3];
        }

        //mature
        if (currentGrowthIndex == 4 && (seasonIndex == 0 || seasonIndex == 1))
        {
           //spring,summer sprite 
           spriteRenderer.sprite = sprites[8];
        }
        if (currentGrowthIndex == 4 && seasonIndex == 2)
        {
           //fall sprite 
           spriteRenderer.sprite = sprites[7];
        }
        if (currentGrowthIndex == 4 && seasonIndex == 3)
        {
           //winter sprite 
           spriteRenderer.sprite = sprites[6];
        }
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
        currentPrefab.transform.parent = transform;
        currentPrefab.transform.position = transform.position;
        currentPrefab.transform.position += positions[currentGrowthIndex];
    }
}
