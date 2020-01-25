using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameMaster gm;
    
    private BoxCollider2D box;
    public bool hero=false;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
      //  box = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))//(collision.gameObject.tag=="Hero")//(collision.CompareTag("Hero"))
        {
            //box.enabled = true;
            gm.lastCheckpointPos = transform.position;
            hero = true;
           // box.enabled = false;
            
        }
    }
}
