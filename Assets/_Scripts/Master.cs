using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Master : MonoBehaviour
{
    public GameObject newRoot;
    public GameObject snake;
    public List<GameObject> roots = new();
    public Stats stats = new();
    public Perks perks = new();


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
    private bool m_coldResist;
    private bool m_heatResist;
    public bool coldResist
    {
        get { return m_coldResist; }
        set { m_coldResist = value; }
    }
    public bool heatResist
    {
        get { return m_heatResist; }
        set
        {
                m_heatResist = value;
        }
    }
}


public class Stats
{
    private int m_water;
    private int m_nitrogen;
    private int m_light;
    public int water
    {
        get { return m_water; }
        set { m_water = value; }
    }
    public int nitrogen
    {
        get { return m_nitrogen; }
        set { m_nitrogen = value; }
    }
    public int light
    {
        get { return m_light; }
        set { m_light = value; }
    }
}

