﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  public  bool controllable = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        float speed = 0.2f;
        if (controllable)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = transform.position + new Vector3(0, speed, 0);

            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = transform.position + new Vector3(0, -speed, 0);

            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = transform.position + new Vector3(-speed, 0, 0);

            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = transform.position + new Vector3(speed, 0, 0);

            }
        }
    }
}