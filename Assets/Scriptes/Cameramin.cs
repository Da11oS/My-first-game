using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramin : MonoBehaviour
{
   
    public Transform pers;
    public float speed;
    internal object main;
    private GameObject Hero;
    Vector3 position;
    Quaternion CamRotate;
   
   //Camera cam;
     void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
        transform.position = new Vector3(Mathf.Clamp(Hero.transform.position.x,-8,144.5f),transform.position.y,transform.position.z);
      //  FindPlayer(isleft);
       //cam = Camera.main;
    }

    // Update is called onceww per frame
    void Update()
    {
       transform.Translate(Vector2.right * speed * Time.deltaTime);
       // transform.position.x = pers.transform.position.x;
     //   transform.position.y = pers.transform.position.x;
    }
   
}
