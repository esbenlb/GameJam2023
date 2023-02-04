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

    public int[] treeModifiers;
    private int[] rootStats;
    private int[] perksStats;

    void Start()
    {
        InvokeRepeating("UpdateEverySecond", 0.0f, 1.0f);
    }

    void UpdateEverySecond()
    {
        GetTreeModifiers();
        stats.water += treeModifiers[0];
        stats.nitrogen += treeModifiers[1];
        stats.light += treeModifiers[2];
        stats.co2 += treeModifiers[3];

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
        if (stats.water >= 100 && perks.heatResist == false)
        {
            stats.water -= 100;
            perks.coldResist = true;
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

