using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeToStartBoss : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject boss,t,hero;
    public GameObject[] Guns = new GameObject[2];
   private Text Timer;
    
    void Start()
    {
        boss = GameObject.Find("boss");
        hero = GameObject.Find("Body");
       // t = GameObject.Find("Timer");
        Timer = GetComponent<Text>();
        boss.GetComponent<Boss>().enabled = false;
        hero.GetComponent<Hero>().enabled = false;
        hero.GetComponent<JumpHero>().enabled = false;
        for (int i = 0; i < 2; i++)
            Guns[i].GetComponent<Gun2>().enabled = false;
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator time()
    {
        int i = 3;
        while (i > 0)
        {
            Timer.text = i.ToString();
            i--;
            yield return new WaitForSeconds(1);
        }
        boss.GetComponent<Boss>().enabled = true;
        hero.GetComponent<Hero>().enabled = true;
        hero.GetComponent<JumpHero>().enabled = true;
        Invoke("GunStart", 2);
        // t.SetActive(false);
        gameObject.SetActive(false);
    }
    void GunStart()
    {
        for (int i = 0; i < 2; i++)
            Guns[i].GetComponent<Gun2>().enabled = true;
    }
}
