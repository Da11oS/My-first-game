using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextScene;
    public static GameMaster gm;
    private GameObject GM;
    public GameObject H;
   // private GameObject Helper;
    public Vector2 HelpVector;
   // private Text Helper;
    public bool checkpoints;
    private GameObject AudioPlayer;
    void Start()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");
        if (checkpoints)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            GM = GameObject.FindGameObjectWithTag("GM");
        }
        H.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       // H.transform.position = HelpVector;
    }
    private void OnTriggerStay2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Hero")
        {

           
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioPlayer.GetComponent<AudioSource>().volume = 0.3f;
                AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().NextLevel);
                if (checkpoints)
                    GM.SetActive(false);
                SceneManager.LoadScene(nextScene);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Hero")
        {
            if (H == null)
            {
              //  Helper = Instantiate(H, new Vector2(transform.position.x, transform.position.y + 5), Quaternion.identity);
                Debug.Log("Помоoи нет");
            }
            else
                H.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Hero")
        {
            if(H!=null)
                H.SetActive(false);
        }
    }
}
