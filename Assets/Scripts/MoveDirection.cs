using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirection : MonoBehaviour
{

    public Transform needle;
    private Vector3 dir = Vector3.zero;
    public GameObject player;
    public RectTransform point;

 
    public void Update()
    {
        dir = player.GetComponent<Rigidbody>().velocity;

        point = GetComponent<RectTransform>();

        point.anchoredPosition = dir;
      
    }

}
