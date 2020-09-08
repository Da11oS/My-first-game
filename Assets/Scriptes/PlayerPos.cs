using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPos : MonoBehaviour
{
    public static GameMaster gm;
    public static float shift=0;
    public GameObject pauseMenu;
    public GameObject Camera;
    public Vector3 restartPos;
    public bool checkpoints;

    void Start()
    {

            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            gm.dead = false;
        pauseMenu.SetActive(false);
            if (!gm.restart)
                transform.position = gm.lastCheckpointPos;
            else if (gm.restart)
                transform.position = restartPos;
            if(checkpoints)
            Camera.transform.position = new Vector3(transform.position.x + Camera.GetComponent<Cameramin>().changePos.x, Camera.GetComponent<Cameramin>().changePos.y, Camera.GetComponent<Cameramin>().changePos.z);
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "Checkpoint")
        {
           gm.lastCheckpointPos = gameObject.transform.position;
        }
    }
   


}
