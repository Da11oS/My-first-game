using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAinmenu : MonoBehaviour
{
    public void PLayGame()
    {
        cl = true;
        SceneManager.LoadScene("1thScene");

    }
    public bool cl = false;
    public void ExitGame()
    {
        cl = true;
        //SceneManager.;
       
    }
}
