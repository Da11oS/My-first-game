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
    private GameObject pauseMenu, textcoin;
    Text txt;
    public Slider slider;
    private float startSpeed;
    void Start()
    {
        //lastpoint = transform.position;
        pauseMenu = GameObject.Find("PauseMenu");
        textcoin = GameObject.Find("TextCoin");

        txt = textcoin.GetComponent<Text>();
        txt.text = "x" + coin.ToString();
        Rebe = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        startSpeed = speed;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    int j_out = 0;
    void FixedUpdate()
    {
        diff = pos - Rebe.transform.position.y;// eсли подним -
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
    void dead()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D shit)
    {
        if (j_out == 1)
            j_out--;
        if (Input.GetKeyDown(KeyCode.S))
        {
            CapsuleCollider2D coll;
            coll = shit.gameObject.GetComponent<CapsuleCollider2D>();
            coll.isTrigger = true;
        }
        if (shit.gameObject.tag == "Batut")
            Rebe.AddForce(transform.up * butjump, ForceMode2D.Impulse);
      
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


    private void OnTriggerStay2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Door")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                life = 5;
               SceneManager.LoadScene("2thScene");
            }

        }
    }
    void anim()
    {
        if (Input.GetAxis("Horizontal") == 0)
            ani.SetInteger("NumberAni", 0);
        else
        {

            flip();
            ani.SetInteger("NumberAni", 2);

        }
        if (diff < 0 && j_out > 0) ani.SetInteger("NumberAni", 1);
        else if (diff > 0 && j_out > 0) ani.SetInteger("NumberAni", 3);
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxis("Horizontal") != 0 && j_out == 0) ani.SetInteger("NumberAni", 1);


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
            coin++;
            Destroy(shit.gameObject);
            txt.text = "x" + coin.ToString();

        }

        if (shit.gameObject.tag == "Blade" && !invul)
        {
            wait = 1.5f;
            invul = true;
            life--;

        }

         if (shit.gameObject.tag == "Checkpoint")
         {
            PlayerPos.gm.lastCheckpointPos = shit.gameObject.transform.position;
            check = true;

         }
       
    }

    void Envole()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
            j++;
            Rebe.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        }
        else if (wait <= 0.9f)
        {
            invul = false;
            Rebe.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

        }
    }

    void GTCheckpoint()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

    }
}

