﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public int targetFloor = 2;
    public int currentFloor = 1;
    public int FloorMax = 3;

    float[] convert = { 0, 2, 5, 8 };

    float Kp = 0.01f;
    bool move = true;
    bool up = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per
    void Update()
    {
        if(move) transform.Translate(0, (convert[targetFloor] - transform.position.y) * Kp, 0);

        if(Mathf.Abs(transform.position.y - convert[targetFloor]) < 0.01f)
        {
            currentFloor = targetFloor;
            if (up)
            {
                if (FloorMax > targetFloor) targetFloor++;
                else up = false;
            }
            else
            {
                if (targetFloor > 1) targetFloor--;
                else up = true;
            }
        }
    }
}