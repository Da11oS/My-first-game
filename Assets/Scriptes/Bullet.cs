using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=5f;
    public float timedead = 3f;
    public float impulse =3f;
    private Rigidbody2D Bull;
    
  
    void Start()
    {
        Bull = GetComponent<Rigidbody2D>();
        Bull.AddForce(transform.up * impulse, ForceMode2D.Impulse);
        Invoke("dead",timedead);
      
    }

    // Update is called once per frame
    void dead()
    {
        Destroy(this.gameObject);
    }
}
