using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator CamAnim;
    private GameObject MainCamera;
    public int bossHealth=10;
    private float waittime,AttackTime;
    public float starttime, SideSpeed, speedTransformRotate, SpikeUpperDistance, StopTransformDistance;
    public float speed, impulse, SpikeUpperImpulse, SpikeSiedeX, SpikeSideDistance, SpikeUpperMaximum, LeftLine, RightLine;
    public Transform[] m_spots;
    public int AttackType;
    bool[] redactRightRotate = new bool[3], redactLeftRotate = new bool[3], WorkDownSpike = new bool[3];
    Quaternion rot;
    public float gg = 150f;
    private int Spots;
    private int i = 0, j = 1;
    private GameObject  Player, Gun, SpikeDown, StartSpikeUpper, StartLeftSpike, StartRightSpike, StartDownSpike;
    public GameObject SpikeUp, LeftS, RightS, DownS;
    public GameObject[] SpikeUpper = new GameObject[3], LeftSpike = new GameObject[3], RightSpike = new GameObject[3], DownSpike = new GameObject[3];
    private Rigidbody2D GunRb, BossRb;
    private Rigidbody2D[] RbSpikeDown = new Rigidbody2D[3];
    private GameObject StartSideSpike,LeftLineCamera, RightLineCamera;
    private bool LeftCreate = true, RightCreate = true, boolAttackLeft, boolAttackRight, boolAttackUpper, boolAttackBossDown; private int Attack = 0;
    void Start()
    {
        Spots = m_spots.Length;
        BossRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Hero");
        StartSpikeUpper = GameObject.Find("StartSpikeUpper");
        StartLeftSpike = GameObject.Find("StartLeftSpikes");
        StartRightSpike = GameObject.Find("StartRightSpikes");
        StartDownSpike = GameObject.Find("StartDownSpikes");
        LeftLineCamera = GameObject.Find("LeftLineCamera");
        RightLineCamera = GameObject.Find("RightLineCamera");
        for (int i = 0; i < 3; i++)
        {
            redactRightRotate[i] = false;
            redactLeftRotate[i] = false;
            RbSpikeDown[i] = DownSpike[i].GetComponent<Rigidbody2D>();
        }
        BossRb.simulated = false;
        CamAnim = MainCamera.GetComponent<Animator>();
        AttackTime = Random.Range(5, 10);

        //Random.Range(0, 3);
        //StartCoroutine(BossAttack()); 
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
            AttackLeftSpike();
        if (boolAttackRight)
            AttackRightSpike();
        AttackDown();

    }
    private void AttackBool()
    {
        Attack = 0;
    }
    private void BossAttack()
    {
        Attack++;
        AttackTime = Random.Range(5, 10);
        AttackType = Random.Range(0, 3);

            switch (AttackType)
            {
                case 0:
                    AttakSpikeUpper();
                    Invoke("ChenchgSpikeUpperTrnsform", 3);
                    Invoke("DestroyUpperSpike", 13);
                    AttackTime = 13;
                    break;
                case 1:
                    boolAttackLeft = true;
                    break;
                case 2:
                    boolAttackRight = true;
                    break;
                case 3:
                    BossRb.simulated = true;
                    GetComponent<Blade>().enabled = false;
                    break;

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
        SpikeDown.transform.parent = gameObject.transform;
    }
    void AttackLeftSpike()
    {
        for (int i = 0; i < 3; i++)
        {
            if (LeftSpike[i] != null)
            {
                LeftSpike[i].transform.parent = null;
                LeftSpike[i].transform.Translate(LeftSpike[i].transform.up * SideSpeed * Time.deltaTime);
                if (Vector2.Distance(LeftSpike[i].transform.position, Player.transform.position) < StopTransformDistance)
                    redactLeftRotate[i] = true;
                if (!redactLeftRotate[i])
                {
                    Vector2 direction = Player.transform.position - LeftSpike[i].transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle + gg, Vector3.forward);
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
            Invoke("CreateLeftSpike", 3);
        }
    }
    void AttackRightSpike()
    {
        for (int i = 0; i < 3; i++)
        {
            if (RightSpike[i] != null)
            {
                RightSpike[i].transform.parent = null;
                RightSpike[i].transform.Translate(RightSpike[i].transform.up * SideSpeed * Time.deltaTime);
                if (Vector2.Distance(RightSpike[i].transform.position, Player.transform.position) < StopTransformDistance)
                    redactRightRotate[i] = true;
                if (!redactRightRotate[i])
                {
                    Vector2 direction = Player.transform.position - RightSpike[i].transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle + gg, Vector3.forward);
                    RightSpike[i].transform.rotation = Quaternion.Slerp(RightSpike[i].transform.rotation, rotation, speedTransformRotate * Time.deltaTime);
                }
                if (Vector2.Distance(RightSpike[i].transform.position, Player.transform.position) < 0.5)
                {
                    Destroy(RightSpike[i]);

                }
            }
        }
        if (RightSpike[0] == null && RightSpike[1] == null && RightSpike[2] == null && RightCreate)
        {
            RightCreate = false;
            Invoke("CreateRightSpike", 3);
        }
    }
            void CreateLeftSpike()
            {
        LeftCreate = true;
        boolAttackLeft = false;
                LeftSpike[0] = Instantiate(LeftS,
                      new Vector2(StartLeftSpike.transform.position.x + SpikeSiedeX, StartLeftSpike.transform.position.y - SpikeSideDistance), new Quaternion(0, 0, 180, 0));
                LeftSpike[1] = Instantiate(LeftS,
                    new Vector2(StartLeftSpike.transform.position.x + SpikeSiedeX, StartLeftSpike.transform.position.y), new Quaternion(0, 0, 180, 0));
               LeftSpike[2] = Instantiate(LeftS,
                    new Vector2(StartLeftSpike.transform.position.x + SpikeSiedeX, StartLeftSpike.transform.position.y + SpikeSideDistance), new Quaternion(0, 0, 180, 0));
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
                RightSpike[0] = Instantiate(RightS,
                       new Vector2(StartRightSpike.transform.position.x - SpikeSiedeX, StartRightSpike.transform.position.y - SpikeSideDistance), Quaternion.identity);
                RightSpike[1] = Instantiate(RightS,
                    new Vector2(StartRightSpike.transform.position.x - SpikeSiedeX, StartRightSpike.transform.position.y), Quaternion.identity);
                RightSpike[2] = Instantiate(RightS,
                    new Vector2(StartRightSpike.transform.position.x - SpikeSiedeX, StartRightSpike.transform.position.y + SpikeSideDistance), Quaternion.identity);
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
                 if (collision.gameObject.tag == "Gress"|| collision.gameObject.tag == "Hero")
                {
                    BossRb.simulated = false;
                    GetComponent<Blade>().enabled = true;
                    CamAnim.SetInteger("IntShake", 1);
                }
            }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossSideSpike")
            bossHealth--;
    }
    void Move()
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
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }

}
