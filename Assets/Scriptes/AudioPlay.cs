using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public static AudioPlay instance;
    public AudioClip Click,Money,Jump,Damage,Water,Chest, NextLevel;
    public AudioClip DamageBoss;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        else
            Destroy(gameObject);
    }
}
