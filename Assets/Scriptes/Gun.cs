using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject target;
    public static float toat;
    public float speed = 5f, dis=15f;
    public float gg = 150f,dist=30,shoottime=5f;
    public bool BossGun;
    float  posx, posy;
    Quaternion rot;
    public GameObject Bull;
    public GameObject trunk;
    public bool sh = false;

    void Start()
    {
        StartCoroutine(Shoot());

    }
    void Update()
    {
       // rot = Quaternion.identity; 
       // posx = trunk.transform.position.x;
       // posy = trunk.transform.position.y;
        WatchHero();
      

    }
    void WatchHero()
    {
        
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle+gg, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            rot = Quaternion.AngleAxis(angle-90, Vector3.forward);


    }

    IEnumerator Shoot()
    {

        while (true)
        {
            if(Vector2.Distance(transform.position,target.transform.position)<dis)
            Instantiate(Bull, new Vector2(trunk.transform.position.x, trunk.transform.position.y), rot);
            if (BossGun)
                shoottime = UnityEngine.Random.Range(5,15);
            yield return new WaitForSeconds(shoottime);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bg")
            sh = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bg")
            sh = false;

    }*/
}

