using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem sys;
    void Start()
    {
        sys = GetComponent<ParticleSystem>();
        sys.Play();
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
