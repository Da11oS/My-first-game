using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    private float waittime;
    public float starttime;
    public float speed;
    public Transform[] m_spots;
    private int Spots;
    private int i = 0, j = 1;
    void Start()
    {
        Spots = m_spots.Length;
    }

    // Update is called once per frame
    void Update()
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

    }
}