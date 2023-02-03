 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBase : MonoBehaviour
{
    public GameObject[] trunks;
    public GameObject[] crowns;
    List<Transform> children = new();
    void Start()
    {
        children.Add(transform.GetChild(0));
        children[0].GetComponent<SpriteRenderer>().sprite = crowns[2].GetComponent<SpriteRenderer>().sprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
