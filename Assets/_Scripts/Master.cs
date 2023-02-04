using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Master : MonoBehaviour
{
    public GameObject newRoot;
    public GameObject snake;
    public List<GameObject> roots = new();
    public Stats stats = new();
    
}

public class Perks
{
    private bool m_coldResist;
    public bool water
    {
        get { return m_coldResist; }
        set { 
            m_coldResist = value; 
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

