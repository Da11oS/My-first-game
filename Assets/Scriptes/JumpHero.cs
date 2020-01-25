using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHero : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hero;
    private Rigidbody2D heroMove;
    public Transform Graund;
    private float graundRafius = 0.2f;
    public LayerMask isGraund;
    public float  impulse = 7f;
    public bool resolt;
    void Start()
    {
    //    hero = GameObject.FindGameObjectWithTag("Hero");
        heroMove = /*hero.*/GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    Dash();
        resolt = Physics2D.OverlapCircle(Graund.position, graundRafius, isGraund);
       
            if (Input.GetKeyDown(KeyCode.Space) && resolt)
                jump();
              
    }

    void jump()
    {
      //  Debug.Log("Сработал тригер");
        heroMove.velocity = new Vector2(0, 0);
        heroMove.AddForce(transform.up * impulse, ForceMode2D.Impulse);
    }
}
