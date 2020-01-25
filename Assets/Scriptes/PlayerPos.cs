using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPos : MonoBehaviour
{
    public static GameMaster gm;
    public GameObject BG;
    public bool check = false;
    public static float shift=0;
    private GameObject Player;
    public float dis;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        Player = GameObject.Find("Body");
       transform.position = gm.lastCheckpointPos;
        if (Vector2.Distance(transform.position, BG.transform.position) <50)
        {
            var color = BG.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            BG.GetComponent<SpriteRenderer>().color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Hero>().life <= 0)
            Invoke("TrPos", 3);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        { Time.timeScale = 0.5f; Invoke("TrPos", 3); }
    }
    void TrPos()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Player.GetComponent<Hero>().life = 5;
    }
        

}
