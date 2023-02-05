using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVCam : MonoBehaviour
{
    void Update() {
        if(GameObject.FindGameObjectWithTag("Player") != null)
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
