using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Master : MonoBehaviour
{
    float dayLength = 2.0f; // how long a day lasts
    TreeBase treeBase;
    public GameObject newRoot;
    public GameObject snake;
    public GameObject[] resources;
    public List<GameObject> roots = new();
    public Stats stats = new Stats();
    public Perks perks = new();
    public float destroyTimer = 15.0f;
    public float snakeSpeed = 0.05f;
    public int[] treeModifiers;
    public int[] perksModifiers;
    public int[] seasonModifiers;
    public Season currentSeason;
    public int currentSeasonIndex;
    public int daysPassed;
    public int season;
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    void Start()
    {
        GameObject Tree = GameObject.Find("Tree");
        treeBase = Tree.GetComponent<TreeBase>();
        stats.water = 90;
        stats.nitrogen = 100;
        stats.light = 100;
        InvokeRepeating("UpdateLoop", 0.0f, dayLength);
    }
    
    void Update()
    {
        
    }

    public float GetDayLength()
    {
        return dayLength;
    }

    void UpdateLoop()
    {
        daysPassed++;
        season = (daysPassed % 365) / 91;
        if (season != currentSeasonIndex)
        {
            ChangeSeason();
        }

        GetTreeModifiers();
        SeasonModifiers();
        stats.water += treeModifiers[0] + seasonModifiers[0];
        stats.nitrogen += treeModifiers[1] + seasonModifiers[1];
        stats.light += treeModifiers[2] + seasonModifiers[2];
        stats.co2 += treeModifiers[3] + seasonModifiers[3];

    }

    public void ChangeSeason()
    {
        currentSeasonIndex = (currentSeasonIndex + 1) % 4;
        currentSeason = (Season)currentSeasonIndex;
        SeasonModifiers();
        treeBase.ChangeSprite(currentSeasonIndex);
    }

    public void SeasonModifiers()
    {
        switch(currentSeason)
        {
            case Season.Winter:
                seasonModifiers = new int[] {-3, -3, 0, 0};
                break;
            case Season.Spring:
                seasonModifiers = new int[] {-1, 1, 2, 2};
                break;
            case Season.Summer:
                seasonModifiers = new int[] {-10, 0, 5, 5};
                break;
            case Season.Autumn:
                seasonModifiers = new int[] {3, 3, 1, 1};
                break;
        }
    }
    public bool SpawnRoot()
    {
        int lightCost = 30;
        if(stats.light >= lightCost)
        {
            stats.light -= lightCost;
            return true;
        }
        return false;
    }

    public void AddBigWater()
    {
        stats.water += 50;
    }
    public void AddSmallWater()
    {
        stats.water += 10;
    }
    public void AddNitrogen()
    {
        stats.nitrogen += 10;
    }
    public void AddLight()
    {
        stats.light += 10;
    }

    public void SeasonWinter()
    {
        stats.light += 0;
        stats.water += 0;
        stats.nitrogen += 0;
    }
    public void SeasonSummer()
    {
        stats.light += 100;
        stats.water += 100;
        stats.nitrogen += 100;
    }
    public void SeasonSpring()
    {
        stats.light += 50;
        stats.water += 50;
        stats.nitrogen += 50;
    }
    public void Seasonfall()
    {
        stats.light += 10;
        stats.water += 50;
        stats.nitrogen += 50;
    }

    private void GetTreeModifiers()
    {
        
        treeModifiers = treeBase.GetModifiers();
    }

    // example funcAddPerk. in this case, can only get perk:coldResist IF has 100 water AND dosen't have heatResist
    public void AddPerkColdResist()
    {
        int cost = 100;
        if (stats.water >= cost && perks.heatResist == false)
        {
            stats.water -= cost;
            perks.coldResist = true;
        }
    }
    public void AddRootGrowSpeed()
    {
        int cost = 25;
        if (stats.nitrogen >= cost)
        {
            stats.nitrogen -= cost;
            snakeSpeed += snakeSpeed / 10;
        }
    }
}

public class Perks
{
    public bool coldResist;
    public bool heatResist;
}


public class Stats
{
    public int water;
    public int nitrogen;
    public int light;
    public int co2;
}

