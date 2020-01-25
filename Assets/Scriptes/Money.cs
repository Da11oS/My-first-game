using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    static public int coin=0;
    private GameObject money;
    public GameObject hero;
    public GameObject textcoin;
    public GameObject chest;
     Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = textcoin.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(hero.transform.position, money.transform.position) < 0.2f)
        {
            coin++;
            txt.text = coin.ToString();
            Destroy(this);
        }
        if (Vector2.Distance(hero.transform.position, chest.transform.position) < 0.3f)
        {
            coin=coin+10;
            txt.text = coin.ToString();        
        }
    }
   
}
