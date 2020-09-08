using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameMaster gm;
    private BoxCollider2D box;
    public bool hero=false;
    public GameObject salut;
    private ParticleSystem salutPS;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        salutPS = salut.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            
            gm.lastCheckpointPos = transform.position;
            hero = true;
            salutPS.Play(true);
        }
    }
}
