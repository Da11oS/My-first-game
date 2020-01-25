using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat_Down : MonoBehaviour
{
    // Start is called before the first frame update
    public float power=5f;
    private Rigidbody2D reb;
    private Rigidbody2D col;
    public bool dup = false,otcsh=false;
    float timeto = 1f;
    public float stop,stopup,otpow;
    public float mas;
       void Start()
    {
        reb = GetComponent<Rigidbody2D>();
        mas = reb.mass;
    }

    // Update is called once per frame
    void Update()
    {

        moveUp();
    }
    void moveUp()
    {
        if(transform.position.y<stop&&dup)
        transform.Translate(Vector2.up * power/2 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.transform.parent = gameObject.transform;
                dup = false;
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            if(transform.position.y > stopup)
            transform.Translate(Vector2.down * power / 2 * Time.deltaTime);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            dup = true;
                collision.gameObject.transform.parent = null;
        }
    }
    void Timing()
    {
        timeto -= Time.deltaTime;
    }


}
