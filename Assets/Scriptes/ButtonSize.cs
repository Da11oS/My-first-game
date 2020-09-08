using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonSize : MonoBehaviour
{
    // Start is called before the first frame update
    private Text r;
    private GameObject AudioPlayer;
    private void Start()
    {
        r = GetComponent<Text>();
        AudioPlayer = GameObject.Find("AudioPlayer");
      //  Click = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
    }
    private void OnMouseEnter()
    {
        r.fontSize = 200;
      
    }
    private void OnMouseExit()
    {
        r.fontSize = 180;
    }
}
