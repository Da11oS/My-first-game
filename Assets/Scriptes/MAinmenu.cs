using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAinmenu : MonoBehaviour
{
    private GameObject AudioPlayer;
    public GameObject IExitHelper, QHelperText, Helper,GoToPlay;
    public void PLayGame()
    {
        cl = true;
        SceneManager.LoadScene("1thScene");
        
    }
    public void OpenHelper()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        
        Helper.SetActive(true);
        IExitHelper.SetActive(true);
        GoToPlay.SetActive(false);Debug.Log("?");
        QHelperText.SetActive(false);
        
    }
    public void CloseHelper()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        QHelperText.SetActive(true);
        Helper.SetActive(false);
        Debug.Log("I");
        GoToPlay.SetActive(true);
        IExitHelper.SetActive(false);
        
    }
    public bool cl = false;
    public void ExitGame()
    {
        AudioPlayer = GameObject.Find("AudioPlayer");
        AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Click);
        cl = true;
        //SceneManager.;
        Application.Quit();
    }
}
