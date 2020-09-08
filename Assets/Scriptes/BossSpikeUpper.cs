using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikeUpper : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Boss;
    private GameObject AudioPlayer;
    void Start()
    {
        Boss = GameObject.Find("boss");
    }

    // Update is called once per frame
    void Update()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");

        if (transform.position.x < Boss.transform.position.x + (4.11f / 2)//-1
            && transform.position.x > Boss.transform.position.x - (4.11f / 2)//+1
            && transform.position.y < Boss.transform.position.y + (6 / 2)//-1
             && transform.position.y > Boss.transform.position.y - (6 / 2)//+1
            )
        {
            AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().DamageBoss);
            Boss.GetComponent<Boss>().bossHealth--;
            Debug.Log("Урон по Боссу.");
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boss")
        {
            //AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().DamageBoss);
            //Boss.GetComponent<Boss>().bossHealth--;
            //Debug.Log("Урон по Боссу.");
            //Destroy(gameObject);
           
        }
    }
}
