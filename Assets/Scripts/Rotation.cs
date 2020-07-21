using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject point;

    private double dx;
    private double dy;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        dx = point.GetComponent<RectTransform>().anchoredPosition.x;
        dy = 0;

        float sdx = (float)dx;
        float sdy = (float)dy;
        float rotatespeed = 100f;

        float sR = Mathf.Atan2(sdx, sdy);
        float sD = 360 * sR / (3.5f * Mathf.PI);
        
        float startAngle_y = sD;
      
        Quaternion target = Quaternion.Euler(0, 0,-sD);
        if(dx >= 0.1f || dx <=-0.1)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * rotatespeed);
        else transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime * rotatespeed);
    }
}