using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystem : MonoBehaviour
{

    public float water;
    public float nitrogen;
    public float light;
    public float co2;

    private float[] treeStats;
    private float[] rootStats;
    private float[] perksStats;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Stats");
        GameObject Tree = GameObject.Find("Tree");
        TreeBase treeBase = Tree.GetComponent<TreeBase>();
        treeBase.Test();
    }

    // Update is called once per frame
    void Update()
    {
        //water += 2;
        //
        //Debug.Log("W: " + water);
        //Debug.Log("N2: " + nitrogen);
        //Debug.Log("L: " + light);
        //Debug.Log("CO2: " + co2);
    }

    public void setTreeStats(float[] newStats)
    {
        treeStats = newStats;
    }
}
