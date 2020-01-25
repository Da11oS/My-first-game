using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHero : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hero;
    private Rigidbody2D heroMove;
    public Transform Graund;
    private float graundRafius = 0.2f;
    public LayerMask isGraund;
    public float  impulse = 7f;
    public bool resolt;
    private float scaleX, scaleY, PlatformScaleX, PlatformScaleY;
    private bool onPlatform = false;
    void Start()
    {
    //    hero = GameObject.FindGameObjectWithTag("Hero");
        heroMove = /*hero.*/GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    Dash();
        resolt = Physics2D.OverlapCircle(Graund.position, graundRafius, isGraund);
       
            if (Input.GetKeyDown(KeyCode.Space) && resolt)
                jump();
           transform.localScale = new Vector3(scaleX, scaleY, 0);
        if(onPlatform)
            transform.localScale = new Vector3(scaleX / PlatformScaleX, scaleY / PlatformScaleY, 0);

    }

    void jump()
    {
        heroMove.velocity = new Vector2(0, 0);
        heroMove.AddForce(transform.up * impulse, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            PlatformScaleX = collision.gameObject.transform.localScale.x;
            PlatformScaleY = collision.gameObject.transform.localScale.y;
            onPlatform = true;
            gameObject.transform.parent = collision.gameObject.transform;
           // transform.localScale= new Vector3(scaleX / collision.transform.localScale.x, scaleY / collision.transform.localScale.y, 0);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = null;
            onPlatform = false;
        }
    }
}
