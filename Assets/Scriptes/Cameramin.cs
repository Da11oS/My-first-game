using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cameramin : MonoBehaviour
{
    public static GameMaster gm;
    public Transform pers;
    public float speed,timer,startspeed;
    internal object main;
    private GameObject hero;
    private GameObject t;
    private Text Timer;
    bool start=false;
    Vector3 position;
    public Vector3 changePos;
    private float posZ;
    Quaternion CamRotate;
    int i = 3;
    //Camera cam;
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        t = GameObject.Find("Timer");
        Timer = t.GetComponent<Text>();
        hero.GetComponent<Hero>().enabled = false;
        posZ = transform.position.z;
       // transform.position = pers.transform.position+changePos;
        transform.position = new Vector3(/*Mathf.Clamp(hero.transform.position.x,-8,133f)*/hero.transform.position.x, 0.27f, transform.position.z);
        StartCoroutine(time());
        startspeed = speed;
    }

    IEnumerator time()
    { 
        while(i>0)
        {
            //iText = ToString(i);
            Timer.text = i.ToString();
            Debug.Log("i="+i);
            if (t != null)
                Debug.Log("Timer Find!");
            else Debug.Log("Timer NOT Find!");
            i--;
            yield return new WaitForSeconds(timer);
        }
        start = true;
        hero.GetComponent<Hero>().enabled = true;
        t.SetActive(false);
    }
    // Update is called onceww per frame
    void Update()
    {
        if(transform.position.x<133&&start)
       transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x < hero.transform.position.x && speed < startspeed+2)
            speed +=0.2f*Time.deltaTime ;
        else if(transform.position.x < hero.transform.position.x && speed > startspeed - 2) speed -= 0.2f * Time.deltaTime;
        //    
    }
   
}
