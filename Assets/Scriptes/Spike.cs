using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameObject hero;
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        GetComponent<Rigidbody2D>().simulated = false;

    }

      // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - hero.transform.position.x) < 0.5f && transform.position.y > hero.transform.position.y)
            GetComponent<Rigidbody2D>().simulated = true;

    }
}
