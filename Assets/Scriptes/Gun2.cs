using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float raydist = 10f, shootZero = 2f, shoottime =2f;
    public Transform rayDist;
    public GameObject Boy, Bull, trunk;
    public bool spot = false, spottrue=false;
    Quaternion rot; 
    float numBull = 1;
    void Start()
    {   
       StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up*raydist, Color.red);
        spot = Physics2D.Linecast(transform.position, rayDist.position,1<<LayerMask.NameToLayer("Body"));
        rot = transform.rotation;
        if (spot)
            spottrue = true;
      /*  if (spottrue)
        {
            Instantiate(Bull, new Vector2(trunk.transform.position.x, trunk.transform.position.y), rot);
            spottrue = false;
        }*/
        //Create_Bull();
        // shoottime -= Time.deltaTime;
        /*
        if (shoottime <= 0)
        {
            Instantiate(Bull, new Vector2(trunk.transform.position.x, trunk.transform.position.y), rot);
            shoottime = shootZero;
        }*/

    }
    void Num1()
    {
        numBull = 1;
    }
    void Create_Bull()
    {
        if (numBull == 1)
        {
            Instantiate(Bull, new Vector2(trunk.transform.position.x, trunk.transform.position.y), rot);
            numBull = 0;
        }

    }
   IEnumerator shoot()
    {
        while (true)
        {
            if (spottrue)
            {
                Instantiate(Bull, new Vector2(trunk.transform.position.x, trunk.transform.position.y), rot);
                spottrue = false;
                yield return new WaitForSeconds(shoottime);
            }
             else yield return new WaitForSeconds(0.3f);
        }
    }
}
