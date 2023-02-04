using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //  Audio refs
    public  AudioSource clickSound;
    public  AudioSource hoverSound;
    
    public void HoverSound() {
        hoverSound.Play();
    }
    public void ClickSound() {
        hoverSound.Play();
    }
}
