using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Master : MonoBehaviour
{
    public GameObject newRoot;
    public GameObject snake;
    public GameObject[] resources;
    public List<GameObject> roots = new();
    public Stats stats = new Stats();
    public Perks perks = new();
    public float destroyTimer = 5.1f;
    public float speed = 0.03f;
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
        stats.water = 90;
        stats.nitrogen = 100;
        stats.light = 100;
        InvokeRepeating("UpdateEverySecond", 0.0f, 1.0f);
    }
    
    void Update()
    {
        daysPassed++;
        season = (daysPassed % 365) / 91;
        if (season != currentSeasonIndex)
        {
            ChangeSeason();
        }
    }

    void UpdateEverySecond()
    {
        GetTreeModifiers();
        stats.water += treeModifiers[0];
        stats.nitrogen += treeModifiers[1];
        stats.light += treeModifiers[2];
        stats.co2 += treeModifiers[3];

    }

    public void ChangeSeason()
    {
        currentSeasonIndex = (currentSeasonIndex + 1) % 4;
        currentSeason = (Season)currentSeasonIndex;
        SeasonModifiers();
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
        int waterCost = 30;
        if(stats.water >= waterCost)
        {
            stats.water -= waterCost;
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

    private void GetTreeModifiers()
    {
        GameObject Tree = GameObject.Find("Tree");
        TreeBase treeBase = Tree.GetComponent<TreeBase>();
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
        int cost = 100;
        if (stats.water >= cost)
        {
            stats.water -= cost;
            speed += speed / 10;
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

