using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    private Rigidbody2D Rebe;
    private Animator ani;
    public Text txt;
    public float dashSpeed=10f,timeDash,speed = 5f,butjump=10f;
    public float lspeed = 5;
    public  int coin = 0, life = 5,p1;
    public float wait = 1.5f;
    bool invul = false;
    float pos, diff;
    public int j = 0;
    private float scaleX,scaleY;
    static public Vector2 lastpoint;
    public bool check=false;
    private GameObject  textcoin;
    public bool Dead;
    public Slider slider;
    private float startSpeed;
    public GameObject PS;
    private GameObject AudioPlayer;
    void Start()
    {
        textcoin = GameObject.Find("TextCoin");
        txt = textcoin.GetComponent<Text>();
        txt.text = "x" + coin.ToString();
        Rebe = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        startSpeed = speed;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        Dead = false;
        AudioPlayer = GameObject.Find("AudioPlayer");
        AudioPlayer.GetComponent<AudioSource>().volume = 1;
        //  PS.SetActive(false);

    }

   
    void FixedUpdate()
    {
        diff = pos - Rebe.transform.position.y;
        run();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = dashSpeed;
            Invoke("speedReturn", timeDash);
        }
        pos = Rebe.transform.position.y;
        if (invul)
            Envole();
        anim();
        slider.value = lspeed;
        if (lspeed > life)
            lspeed -= 0.015f;
        transform.localScale = new Vector3(scaleX, scaleY, 0);
        if (life <= 0)
        {
            speed = 0;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<SpriteRenderer>().enabled = false;
           // PS.SetActive(true);
            PS.GetComponent<ParticleSystem>().Play(true);
            
            Time.timeScale = 0.5f;
            Invoke("TrPos", 1.5f);
        }
    }
    void speedReturn()
    {
        speed = startSpeed;
    }
    void run()
    {
        Rebe.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Rebe.velocity.y);
    }
    void flip()
    {
        if (Input.GetAxis("Horizontal") < 0)
            Rebe.transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (Input.GetAxis("Horizontal") > 0)
            Rebe.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }
    private void OnCollisionEnter2D(Collision2D shit)
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            CapsuleCollider2D coll;
            coll = shit.gameObject.GetComponent<CapsuleCollider2D>();
            coll.isTrigger = true;
        }
        if (shit.gameObject.tag == "Batut")
            Rebe.AddForce(transform.up * butjump, ForceMode2D.Impulse);
      

    }
    public void TrPos()
    {
        GetComponent<Hero>().Dead = true;
        GetComponent<PlayerPos>().pauseMenu.SetActive(true);
        Destroy(gameObject);
    }
    public void OnDestroy()
    {
       // PS.transform.parent = null;
       
    }
    private void OnCollisionStay2D(Collision2D shit)
    {
        if (shit.gameObject.tag == "Platform")
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                CapsuleCollider2D coll;
                coll = shit.gameObject.GetComponent<CapsuleCollider2D>();
                coll.isTrigger = true;
            }
        }
    }   
    void anim()
    {
        if (Input.GetAxis("Horizontal") == 0 && GetComponent<JumpHero>().resolt)
            ani.SetInteger("NumberAni", 0);
        else
        {
            
            flip();
            if (GetComponent<JumpHero>().resolt)
            ani.SetInteger("NumberAni", 2);
        }
    }
    private void OnTriggerExit2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Platform")
        {
            CapsuleCollider2D coll;
            coll = shit.gameObject.GetComponent<CapsuleCollider2D>();
            coll.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Money")
        {
            AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Money);
            coin++;
            Destroy(shit.gameObject);
            txt.text = "x" + coin.ToString();

        }

        if (shit.gameObject.tag == "Blade" || shit.gameObject.tag == "BossSideSpike" )
        {
            AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Damage);
            if (!invul)
            {
                wait = 2;
                invul = true;
                life--;
                j = 0;
            }
        }
        if (shit.gameObject.tag == "Water")
        {
            AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Water);
            life = 0;
           // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            slider.value = life;
        }
    }

    void Envole()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
            j++;
            if(j%2==0)
            Rebe.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            else Rebe.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
        else if (wait <= 0.01f)
        {
            invul = false;
            Rebe.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

        }
    }
}

