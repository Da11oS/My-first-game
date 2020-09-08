using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHero : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hero;
    public  bool jumpAni;
    private Rigidbody2D heroMove;
    private Animator ani;
    public Transform Graund;
    private float graundRafius = 1f;
    public LayerMask isGraund;
    public float  impulse = 7f, fpsDistance;
    public bool resolt;
    private float scaleX, scaleY, PlatformScaleX, PlatformScaleY;
    private bool onPlatform = false;
    float pos, diff, lastPosition;
    int j_out = 0;
    private GameObject AudioPlayer;
  
    void Start()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");
    //    hero = GameObject.FindGameObjectWithTag("Hero");
        heroMove = /*hero.*/GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        ani = GetComponent<Animator>();
        lastPosition = transform.position.x;
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
        diff = transform.position.y - lastPosition;
        lastPosition = transform.position.y;
        AnimatioN();
    }

    void jump()
    {
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Jump);
        heroMove.velocity = new Vector2(0, 0);
        heroMove.AddForce(transform.up * impulse, ForceMode2D.Impulse);
    }

    void AnimatioN()
    {
        if (!resolt)
        {
            if (diff > 0/* && Mathf.Abs(diff) < fpsDistance*/)
            {
                jumpAni = true;
                ani.SetInteger("NumberAni", 1);
            }
            else if (diff < 0 /*&& Mathf.Abs(diff) < fpsDistance*/)
            {
                ani.SetInteger("NumberAni", 3);
                jumpAni = true;
            }
            else
            {
                jumpAni = false;

            }
        }
      //  if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxis("Horizontal") != 0 && j_out == 0) ani.SetInteger("NumberAni", 1);
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
        if (j_out >= 1)
            j_out--;
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
