using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolingAmera : MonoBehaviour
{
    // Start is called before the first frame update
    public bool scrolling, paralax;
    public float bgSize, paralaxSpeed;
    public Transform CamTransform;
    private Transform Cam1Transform;
    public float camX,camY;
    private Transform[] layers;
    private float viewZone = 10f;
    private int leftId, rightId;
    private float lastCameraX;
    void Start()
    {
        CamTransform = Camera.main.transform;
        lastCameraX = CamTransform.position.x;
        camX = CamTransform.position.x;
        camY = CamTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (paralax)
        {
            float DeltaX = CamTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (DeltaX * paralaxSpeed * Time.deltaTime);
            lastCameraX = CamTransform.position.x;
        }
        if(scrolling)
        {
            if (CamTransform.position.x < layers[leftId].transform.position.x + viewZone)
                ScrollLeft();
            if (CamTransform.position.x > layers[leftId].transform.position.x + viewZone)
                ScrollRight();
        }
    }
    private void ScrollLeft()
    {
        int lastR = rightId;
        layers[rightId].position = Vector3.right * (layers[leftId].position.x - bgSize);
        leftId = rightId;
        rightId--;
        if (rightId < 0)
            {
            rightId = layers.Length - 1;
        }

    }
    private void ScrollRight()
    {
        int lastl = leftId;
        layers[leftId].position = Vector3.right * (layers[rightId].position.x + bgSize);
        rightId = leftId;
        leftId++;
        if (leftId == layers.Length)
        {
            leftId = layers.Length - 1;
        }
    }
}
