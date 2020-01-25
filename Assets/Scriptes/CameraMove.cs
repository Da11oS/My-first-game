using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float dumping = 1.5f, botton_line, upper_line, leftLine, rightLine;
    public Vector2 offsetL = new Vector2(2f, 1f);
    public Vector2 offsetR = new Vector2(2f, 1f);
    public bool isleft;
    public Transform player;
    private int lastX;
    public bool Working = true, leftdownx, leftdowny, rightupx, rightupy;
   
    void Start()
    {
        offsetL = new Vector2(Mathf.Abs(offsetL.x), offsetL.y);
        offsetR = new Vector2(Mathf.Abs(offsetR.x), offsetR.y);
     //   target=new Vector3(Mathf.Clamp(player.position.x - offsetR.x, leftLine, rightLine), Mathf.Clamp(player.position.y - offsetR.y, botton_line, upper_line), transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {


        int currentX = Mathf.RoundToInt(player.transform.position.x);
        if (currentX > lastX) isleft = false;
        else if (currentX < lastX) isleft = true;
        lastX = Mathf.RoundToInt(player.transform.position.x);
        Vector3 target;

        if (isleft)
        {
           target = new Vector3(Mathf.Clamp(player.position.x - offsetR.x, leftLine, rightLine), Mathf.Clamp(player.position.y - offsetR.y, botton_line, upper_line), transform.position.z);

        }
        else
            target = new Vector3(Mathf.Clamp(player.position.x + offsetR.x, leftLine, rightLine), Mathf.Clamp(player.position.y - offsetR.y, botton_line, upper_line), transform.position.z);
        //(player.position.x + offsetR.x, player.position.y - offsetR.y, transform.position.z);


        Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
        transform.position = currentPosition;

    }
}
    /*
    public void IsLeft()
    {
        lastX = Mathf.RoundToInt(player.transform.position.x);
        if (isleft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);

        }
        else transform.position = new Vector3(player.position.x + offset.x, player.position.y - offset.y, transform.position.z);
    }
    */
