using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamma : MonoBehaviour
{
    public GameObject Bg;
    private SpriteRenderer Rend;
    private BoxCollider2D col;
    public Transform point, way;
    private bool start = false;
    public float dist = 0, sp = 2f, run = 10, form = 255;
    void Start()
    {

        Rend = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //form = dist* sp / 255  * Time.deltaTime;
        //dist = point.transform.position.x - way.transform.position.x;
        if (Mathf.Abs(Bg.transform.position.x - transform.position.x) < dist)
        //   if(start)
        {
            var color = Rend.color;
            color.a -= sp * Time.deltaTime;
            Mathf.Clamp(color.a, 0, 1);
            //          form-= 5f*Time.deltaTime;
            //       Mathf.Clamp(form, 0, 255); 
            //         Rend.color = new Color(255, 255, 255,form/*Mathf.Lerp( 0,255,sp)*/);
            ////       RendBg.color = new Color(255,255,255, Mathf.Lerp(255, 0, form));
            Rend.color = color;
             }

        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Camera")//(collision.CompareTag("Hero"))
            {
                col.enabled = false;
                start = true;
            }
        }
    }
