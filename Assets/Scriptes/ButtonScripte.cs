using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonScripte : MonoBehaviour
{
    // Start is called before the first frame update
  //  public static ButtonScripte instance;
    public static GameMaster gm;
    private GameObject Butt, Player;
    public GameObject pauseMenu;
    public bool checkpoints;
    public Vector2 StartPoint;   private GameObject AudioPlayer;
    void Start()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");
        if(checkpoints)
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        pauseMenu.SetActive(false);
        Player = GameObject.Find("Body");
        Time.timeScale = 1;
    }

    void Update()
    {

    }
    public void pause()
    {
        if(pauseMenu!=null)
        Debug.Log("Pausetruew");
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        if (checkpoints)
        {
            gm.restart = true;
            gm.lastCheckpointPos = StartPoint;
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
    public void Continue()
    {
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        if (Player==null)
        {
            if (checkpoints)
                gm.restart = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
            else
        {
                    Time.timeScale = 1;
                    pauseMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        SceneManager.LoadScene("MainMenu");
        pauseMenu.SetActive(false);
    }
}
