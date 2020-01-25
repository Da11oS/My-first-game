using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private GameObject bounce;
    private Rigidbody2D bnc;
    public float jpe = 2f;
    void Start()
    {
        bnc = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         bnc.AddForce(transform.up * jpe, ForceMode2D.Impulse);
    }
}
