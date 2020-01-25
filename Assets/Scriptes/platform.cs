using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platphorm : MonoBehaviour
{
    private float waittime;
    public float starttime;
    public float speed;
    private GameObject Hero;
    public Transform[] m_spots;
    private CapsuleCollider2D coll;
    private int Spots;
    private int i = 0, j = 1;
    public bool back = false;
    
    void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        Spots = m_spots.Length;
        Hero = GameObject.Find("Body");
    }
    private void move()
    {
        if (Spots > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_spots[i].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, m_spots[i].position) < 0.2f)
            {

                if (waittime <= 0)
                {
                    if (i == 0)
                        j = 1;
                    else if (i == Spots - 1)
                        j = -1;
                    i += j;
                    waittime = starttime;
                }
                else
                {
                    waittime -= Time.deltaTime;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S) )
        {
            back = true;
            coll.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Hero.gameObject.transform.parent = gameObject.transform;
            Debug.Log("Сработал триггер на платформу!");

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Hero.gameObject.transform.parent = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.transform.parent = gameObject.transform;
            Debug.Log("Сработал триггер на платформу!");
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
