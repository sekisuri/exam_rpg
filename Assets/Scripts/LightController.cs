﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerManager thePlayer;
    private Vector2 vector;

    private Quaternion rotation;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        this.transform.position = thePlayer.transform.position;
        vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));

        if(vector.x == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            this.transform.rotation = rotation;
        }
        else if(vector.x == -1f)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            this.transform.rotation = rotation;
        }
        else if(vector.y == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            this.transform.rotation = rotation;
        }
        else if(vector.y == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
        }

    }
}
