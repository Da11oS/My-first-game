using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    // Start is called before the first frame update
    public float raydist = 10f;
   
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray2D ray = new Ray2D(transform.position, transform.forward);
        Debug.DrawRay(transform.position, ray.direction * raydist);
    }
   
}
