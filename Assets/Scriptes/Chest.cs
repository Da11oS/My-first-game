﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Animator cani;
   // public Transform hero;
    private GameObject hero;
   
    public bool ch = false;
    BoxCollider2D clChest;
    int pos=0;
    public Sprite CloseChestSprite;
    private GameObject AudioPlayer;
    void Start()
    {
        hero = GameObject.Find("Body");
        AudioPlayer = GameObject.Find("AudioPlayer");
    }

    void Update()
    {
  

        //if (Mathf.Abs(Vector2.Distance(transform.position, hero.transform.position)) < 1 && pos == 0 )
        //{
        //    hero.gameObject.GetComponent<Hero>().coin += 10;
        //    txt.text = "x" + hero.gameObject.GetComponent<Hero>().coin.ToString();
        //    pos = 1;
        //    clChest.enabled = false;
        //    Debug.Log("Сработал тригер");
        //}
        //if (pos == 1)
        //{
        //    cani.SetInteger("ChestA", 2);
        //    pos = 2;
        //}
       
        //else if (pos == 2) cani.SetInteger("ChestA", 1);
        //else  cani.SetInteger("ChestA", 3);

    }

    private void OnTriggerEnter2D(Collider2D shit)
    {
        //if (shit.gameObject.tag == "Chest")
        //{
        //    //coin += 10;
        //    //txt.text = "x" + coin.ToString();
        //    //pos = 1;
        //    shit.gameObject.GetComponent<Animator>().SetInteger("ChestA", 2);
        //    Debug.Log("ChestReaction!");
        //    shit.gameObject.GetComponent<SpriteRenderer>().sprite = CloseChestSprite;
        //    shit.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        //}
        if (shit.gameObject.tag == "Hero")
        {
            AudioPlayer.GetComponent<AudioSource>().PlayOneShot(AudioPlayer.GetComponent<AudioPlay>().Chest);
            shit.GetComponent<Hero>().coin += 10;
            shit.GetComponent<Hero>().txt.text = "x" + shit.GetComponent<Hero>().coin.ToString();
            pos = 1;
            gameObject.GetComponent<Animator>().SetInteger("ChestA", 2);
            Debug.Log("ChestReaction!");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    int t = 0;
void ChangeAnimation()
    {
            gameObject.GetComponent<Animator>().enabled=false ;
    }

}
