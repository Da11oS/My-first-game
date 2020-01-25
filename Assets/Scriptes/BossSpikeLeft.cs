using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikeLeft : MonoBehaviour
{
    // Start is called before the first frame update
    private bool spot;
    public float raydist, speed;
    public Transform rayDist;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.right * raydist, Color.red);
        //spot = Physics2D.Linecast(transform.position, rayDist.position, 1 << LayerMask.NameToLayer("Body"));
        //if (spot)
        //    transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Invoke("DestroySpike", 2);
        }
    }
    void DestroySpike()
    {
        Destroy(this.gameObject);
    }
}
