using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    private Animator CamAnim;
    private GameObject MainCamera;
    public Vector3 leftAxis, RightAxis;
    public Slider BossSlider;
    public int bossHealth=10;
    private float waittime,AttackTime;
    public float starttime, SideSpeed, speedTransformRotate, SpikeUpperDistance, StopTransformDistance, leftAngle, rightAngle;
    public float speed, impulse, SpikeUpperImpulse, SpikeSiedeX, SpikeSideDistance, SpikeUpperMaximum, LeftLine, RightLine;
    public Transform[] m_spots;
    public int AttackType;
    bool[] redactRightRotate = new bool[3], redactLeftRotate = new bool[3], WorkDownSpike = new bool[3];
    Quaternion rot;
    public float gg = 150f;
    private int Spots;
    private int i = 0, j = 1;
    public GameObject LeftLineCamera, RightLineCamera;
    private GameObject Player, SpikeDown;
    public GameObject SpikeUp, LeftS, RightS, DownS;
    public GameObject[] SpikeUpper = new GameObject[3], Gun = new GameObject[2], LeftSpike = new GameObject[3], RightSpike = new GameObject[3], DownSpike = new GameObject[3];
    private Rigidbody2D GunRb, BossRb;
    private Rigidbody2D[] RbSpikeDown = new Rigidbody2D[3];
    public GameObject  StartDownSpike,  StartSpikeUpper, StartLeftSpike, StartRightSpike;
    private bool LeftCreate = true, RightCreate = true, boolAttackLeft, boolAttackRight, boolAttackUpper, boolAttackBossDown; private int Attack = 0;
    private bool[] AttackLeftTimer = new bool[3], AttackRightTimer = new bool[3];
    public GameObject TextWin,PS_Salut,Salut;
    
    void Start()
    {
        BossRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Hero");
        LeftLineCamera = GameObject.Find("LeftLineCamera");
        RightLineCamera = GameObject.Find("RightLineCamera");
        for (int i = 0; i < 3; i++)
        {
            redactRightRotate[i] = false;
            redactLeftRotate[i] = false;
            RbSpikeDown[i] = DownSpike[i].GetComponent<Rigidbody2D>();
        }
        BossRb.simulated = false;
        TextWin.SetActive(false);
        //  CamAnim = MainCamera.GetComponent<Animator>();
        AttackTime = 8;//Random.Range(5, 10);
        // leftAxis = Vector3.up; RightAxis=Vector3.right;
        //Random.Range(0, 3);
        //StartCoroutine(BossAttack()); 
        PS_Salut.GetComponent<ParticleSystem>().Stop(true);
        Salut.SetActive(false);
    }
    public void ChangeSlider()
    {
        BossSlider.value = bossHealth;
    }
    int act;
    void Update()
    {
        //AttackType = 1;
        if(Attack<1)
        {
            BossAttack();
            Invoke("AttackBool", AttackTime);
        }

        if (boolAttackLeft)
        {
            AttackLeftSpike();
        }
        if (boolAttackRight)
            AttackRightSpike();
        AttackDown();
        ChangeSlider();
        if (bossHealth <= 0)
        {
            BossRb.simulated = true;
            Invoke("Win", 3);
            DestroyUpperSpike();
            for (int i = 0; i < 2; i++)
                Gun[i].GetComponent<Gun2>().enabled = false;
                for (int i = 0; i < 3; i++)
            {
                Destroy(DownSpike[i]);
                Destroy(LeftSpike[i]);
                Destroy(RightSpike[i]);
            }
            PS_Salut.GetComponent<ParticleSystem>().Play(true);
            Salut.SetActive(true);
            TextWin.SetActive(true);
        }
    }
    void Win()
    {
        
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        
    }
    private void AttackBool()
    {
        Attack = 0;
    }
    bool UpperAttack = false;
    private void BossAttack()
    {
        if (PS_Salut == null)
            Debug.Log("Кина не будет!");
        Attack++;
        // AttackTime = Random.Range(5, 10);
        if(!UpperAttack)
        AttackType =  Random.Range(0, 4);
        else AttackType = Random.Range(1, 4);
        Debug.Log(AttackType);
       // AttackType = 3;
        switch (AttackType)
            {
                case 0:
                    AttakSpikeUpper();
                    Invoke("ChenchgSpikeUpperTrnsform", 3);
                    Invoke("DestroyUpperSpike", 11);
                UpperAttack = true;
                   //AttackTime = 13f;
                    break;
                case 1:
                for (int i = 0; i < 3; i++)
                {
                    redactLeftRotate[i] = true;
                    
                }
                    boolAttackLeft = true;
                StartCoroutine(LeftAttackInvoke());
                StartCoroutine(redactRotateLeft());
                break;
                case 2:
                for (int i = 0; i < 3; i++)
                {
                    redactRightRotate[i] = true;
                //    AttackRightTimer[i] = false;
                }
                    boolAttackRight = true;
                StartCoroutine(RightAttackInvoke());
                StartCoroutine(redactRotateRight());
                break;
                case 3:
                    BossRb.simulated = true;
                    GetComponent<BoxCollider2D>().isTrigger = false;
                    GetComponent<Blade>().enabled = false;
                Debug.Log("Упал!");
                    break;

            }
        AttackTime = Random.Range(5, 10);
    }
    IEnumerator redactRotateLeft()
    {
        int i = 0;
        while (i < 3)
        {
            redactLeftRotate[i] = false;
            i++;
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator redactRotateRight()
    {
        int i = 0;
        while (i < 3)
        {
            redactRightRotate[i] = false;
            i++;
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator LeftAttackInvoke()
    {
        int i = 0;
        while(i<3)
        {
            AttackLeftTimer[i] = true;
            i++;
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator RightAttackInvoke()
    {
        int i = 0;
        while (i < 3)
        {
            AttackRightTimer[i] = true;
            i++;
            yield return new WaitForSeconds(1);
        }
    }
 
    void AttakSpikeDown()
    {
        if (Player.transform.position.x < SpikeDown.transform.position.x && Player.transform.position.y < SpikeDown.transform.position.y)
            SpikeDown.GetComponent<Rigidbody2D>().simulated = true;
        CreateSpikeDown();
    }
    void AttakSpikeUpper()
    {
        for (int i = 0; i < 3; i++)
        {
            SpikeUpper[i].GetComponent<Rigidbody2D>().simulated = true;
            SpikeUpper[i].GetComponent<Rigidbody2D>().AddForce(transform.up * SpikeUpperImpulse, ForceMode2D.Impulse);
            SpikeUpper[i].transform.parent = null;
        }
    }
    void ChenchgSpikeUpperTrnsform()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SpikeUpper[i].transform.position.y >= SpikeUpperMaximum)
            {
                SpikeUpper[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                SpikeUpper[i].transform.position = new Vector2(Random.Range(LeftLine, RightLine), SpikeUpper[i].transform.position.y);
                SpikeUpper[i].transform.rotation = new Quaternion(0, 0, 180, 0);
            }
        }
    }
    void DestroyUpperSpike()
    {

        for (int i = 0; i < 3; i++)
        {
            UpperAttack = false;
            Destroy(SpikeUpper[i]);
        }
        Invoke("CreateSpikeUpper", 2);
    }
    void AttackDown()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(Player.transform.position.x - DownSpike[i].transform.position.x) < 1 && Player.transform.position.y < DownSpike[i].transform.position.y)
            {
                RbSpikeDown[i] = DownSpike[i].GetComponent<Rigidbody2D>();
                DownSpike[i].transform.parent = null;
                WorkDownSpike[i] = true;
                Invoke("CreateSpikeDown", 5);
                RbSpikeDown[i].simulated = true;
            }
        }

    }
    void CreateSpikeDown()
    {
        for (int i = 0; i < 3; i++)
        {
            if (WorkDownSpike[i])
                Destroy(DownSpike[i]);
        }
        if (WorkDownSpike[0])
        {
            DownSpike[0] = Instantiate(DownS,
                  new Vector2(StartDownSpike.transform.position.x - SpikeUpperDistance, StartDownSpike.transform.position.y), Quaternion.identity);
            DownSpike[0].transform.parent =this.gameObject.transform;
            WorkDownSpike[0] = false;
        }
        if (WorkDownSpike[1])
        {
            DownSpike[1] = Instantiate(DownS,
            new Vector2(StartDownSpike.transform.position.x, StartDownSpike.transform.position.y), Quaternion.identity);
            DownSpike[1].transform.parent = this.gameObject.transform;
            WorkDownSpike[1] = false;
        }
        if (WorkDownSpike[2])
        {
            DownSpike[2] = Instantiate(DownS,
            new Vector2(StartDownSpike.transform.position.x + SpikeUpperDistance, StartDownSpike.transform.position.y), Quaternion.identity);
            DownSpike[2].transform.parent = this.gameObject.transform;
            WorkDownSpike[2] = false;
        }
        for (int i = 0; i < 3; i++)
        {
            DownSpike[i].transform.parent = gameObject.transform;
            RbSpikeDown[i].simulated = false;
        }
        if (SpikeDown.transform.parent == null)
        SpikeDown.transform.parent = gameObject.transform;
    }
    int Li = 0;
    void AttackLeftSpike()
    {
        for (int i = 0; i < 3; i++)
        {
            if (LeftSpike[i] != null)
            {
                //if (Li < 1)
                //{
                //    Invoke("redactRotateLeft", 1);
                //    Li++;
                //}
                if (AttackLeftTimer[i])
                {
                    LeftSpike[i].transform.parent = null;
                    LeftSpike[i].transform.Translate(leftAxis * SideSpeed * Time.deltaTime);
                }
                if (Vector2.Distance(LeftSpike[i].transform.position, Player.transform.position) < StopTransformDistance)
                    redactLeftRotate[i] = true;
                if (!redactLeftRotate[i])
                {
                    Vector2 direction = Player.transform.position - LeftSpike[i].transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle + leftAngle, Vector3.forward);
                    LeftSpike[i].transform.rotation = Quaternion.Slerp(LeftSpike[i].transform.rotation, rotation, speedTransformRotate * Time.deltaTime);
                }
                if (Vector2.Distance(LeftSpike[i].transform.position, Player.transform.position) < 0.5 ||
                    LeftSpike[i].transform.position.x < LeftLineCamera.transform.position.x || LeftSpike[i].transform.position.y > LeftLineCamera.transform.position.y ||
                    LeftSpike[i].transform.position.x > RightLineCamera.transform.position.x || LeftSpike[i].transform.position.y < RightLineCamera.transform.position.y
                    )
                {
                    Destroy(LeftSpike[i]);
                }
            }
        }
        if (LeftSpike[0] == null && LeftSpike[1] == null && LeftSpike[2] == null && LeftCreate)
        {
            LeftCreate = false;
            Li = 0;
            Invoke("CreateLeftSpike", 3);
        }
    }
    int Ri = 0;
    void AttackRightSpike()
    {
        for (int i = 0; i < 3; i++)
        {
            if (RightSpike[i] != null)
            {
                //if (Ri < 1)
                //{
                //    Invoke("redactRotateRight", 1);
                //    Ri++;
                //}
                if (AttackRightTimer[i])
                {
                    RightSpike[i].transform.parent = null;
                    RightSpike[i].transform.Translate(RightAxis * SideSpeed * Time.deltaTime);
                }
                if (Vector2.Distance(RightSpike[i].transform.position, Player.transform.position) < StopTransformDistance)
                    redactRightRotate[i] = true;
                if (!redactRightRotate[i])
                {
                    Vector2 direction = Player.transform.position - RightSpike[i].transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle + rightAngle, Vector3.forward);
                    RightSpike[i].transform.rotation = Quaternion.Slerp(RightSpike[i].transform.rotation, rotation, speedTransformRotate * Time.deltaTime);
                }
                if (Vector2.Distance(RightSpike[i].transform.position, Player.transform.position) < 0.5 ||
                    Vector2.Distance(RightSpike[i].transform.position, Player.transform.position) > 100||
                    RightSpike[i].transform.position.x < LeftLineCamera.transform.position.x || LeftSpike[i].transform.position.y > LeftLineCamera.transform.position.y ||
                    RightSpike[i].transform.position.x > RightLineCamera.transform.position.x || LeftSpike[i].transform.position.y < RightLineCamera.transform.position.y
                    )
                {
                    Destroy(RightSpike[i]);

                }
            }
        }
        if (RightSpike[0] == null && RightSpike[1] == null && RightSpike[2] == null && RightCreate)
        {
            RightCreate = false;
            Ri = 0;
            Invoke("CreateRightSpike", 3);
        }
    }
            void CreateLeftSpike()
            {

        LeftCreate = true;
        boolAttackLeft = false;
        for (int i = 0; i < 3; i++)
            AttackLeftTimer[i] = false;
                LeftSpike[0] = Instantiate(LeftS,
                      new Vector2(StartLeftSpike.transform.position.x, StartLeftSpike.transform.position.y - SpikeSideDistance), new Quaternion(0, 0, 180, 0));
                LeftSpike[1] = Instantiate(LeftS,
                    new Vector2(StartLeftSpike.transform.position.x, StartLeftSpike.transform.position.y), new Quaternion(0, 0, 180, 0));
               LeftSpike[2] = Instantiate(LeftS,
                    new Vector2(StartLeftSpike.transform.position.x, StartLeftSpike.transform.position.y + SpikeSideDistance), new Quaternion(0, 0, 180, 0));
        for (int i = 0; i < 3; i++)
        { 
            LeftSpike[i].transform.parent = gameObject.transform;
            redactLeftRotate[i] = false;
        }
            }
            void CreateRightSpike()
            {
                RightCreate = true;
                boolAttackRight = false;
        for (int i = 0; i < 3; i++)
            AttackRightTimer[i] = false;
        RightSpike[0] = Instantiate(RightS,
                       new Vector2(StartRightSpike.transform.position.x, StartRightSpike.transform.position.y - SpikeSideDistance), Quaternion.identity);
                RightSpike[1] = Instantiate(RightS,
                    new Vector2(StartRightSpike.transform.position.x, StartRightSpike.transform.position.y), Quaternion.identity);
                RightSpike[2] = Instantiate(RightS,
                    new Vector2(StartRightSpike.transform.position.x, StartRightSpike.transform.position.y + SpikeSideDistance), Quaternion.identity);
                for (int i = 0; i < 3; i++)
                {
                RightSpike[i].transform.parent = gameObject.transform;
                redactRightRotate[i] = false;
                }
            }

            void CreateSpikeUpper()
            {
                SpikeUpper[0] = Instantiate(SpikeUp,
                      new Vector3(StartSpikeUpper.transform.position.x - SpikeUpperDistance, StartSpikeUpper.transform.position.y), Quaternion.identity);
                SpikeUpper[1] = Instantiate(SpikeUp,
                    new Vector2(StartSpikeUpper.transform.position.x, StartSpikeUpper.transform.position.y), Quaternion.identity);
                SpikeUpper[2] = Instantiate(SpikeUp,
                    new Vector2(StartSpikeUpper.transform.position.x + SpikeUpperDistance, StartSpikeUpper.transform.position.y), Quaternion.identity);
                for (int i = 0; i < 3; i++)
                    SpikeUpper[i].transform.parent = gameObject.transform;
            }
            private void OnCollisionEnter2D(Collision2D collision)
            {
        if (collision.gameObject.tag == "Gress" || collision.gameObject.tag == "Hero" || collision.gameObject.tag == "Platform")
        {
            BossRb.simulated = false;
            GetComponent<Blade>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
         //   CamAnim.SetInteger("IntShake", 1);
        }
        if (collision.gameObject.tag == "Hero")
            collision.gameObject.GetComponent<Hero>().life--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossSideSpike"|| collision.gameObject.tag == "Blade")
        {
            bossHealth--;
            Destroy(collision.gameObject);
        }
    }
    //void Move()
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position, m_spots[i].position, speed * Time.deltaTime);
    //            if (Vector2.Distance(transform.position, m_spots[i].position) < 0.2f)
    //            {

    //                if (waittime <= 0)
    //                {
    //                    if (i == 0)
    //                        j = 1;
    //                    else if (i == Spots - 1)
    //                        j = -1;
    //                    i += j;
    //                    waittime = starttime;
    //                }
    //                else
    //                {
    //                    waittime -= Time.deltaTime;
    //                }
    //            }
    //            //transform.Translate(Vector2.right * speed * Time.deltaTime);
    //        }

}
