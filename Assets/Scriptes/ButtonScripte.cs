using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonScripte : MonoBehaviour
{
    // Start is called before the first frame update
    public static ButtonScripte instance;
    public GameObject Butt, pauseMenu;
    void Start()
    {
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void pause()
    {

        Time.timeScale = 0;
        //  Butt.SetActive = true;
        Debug.Log("Pausetruew");
        pauseMenu.SetActive(true);


    }
    public void Restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Continue()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMEnu");
        pauseMenu.SetActive(false);
    }
}
