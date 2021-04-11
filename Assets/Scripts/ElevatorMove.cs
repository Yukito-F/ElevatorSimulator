//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ElevatorMove : MonoBehaviour
//{
//    int targetFloor = 2;
//    public int currentFloor = 1;
//    int FloorMax = 5;

//    float[] convert = { 0, 2, 5, 8 , 11, 14};

//    float Kp = 0.02f;
//    public bool move = true;
//    public bool up = true;
//    // Start is called before the first frame update
//    void Start()
//    {
//    }

//    // Update is called once per
//    void Update()
//    {
//        if(move) transform.Translate(0, (convert[targetFloor] - transform.position.y) * Kp, 0);

//        if(Mathf.Abs(transform.position.y - convert[targetFloor]) < 0.01f)
//        {
//            currentFloor = targetFloor;
//            move = false;
//            if (up)
//            {
//                if (FloorMax > targetFloor) targetFloor++;
//                else up = false;
//            }
//            else
//            {
//                if (targetFloor > 1) targetFloor--;
//                else up = true;
//            }
//        }
//    }
//}

using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    int targetFloor = 2;
    public int currentFloor = 1;
    int FloorMax = 5;

    float[] floor2y = { 0, 2, 5, 8, 11, 14 };

    public bool move = true;
    public bool up = true;
    bool actFlag = true;

    GameObject door;

    void Start()
    {
        door = transform.Find("door").gameObject;
    }

    void Update()
    {
        if (move)
        {
            //transform.Translate(0, (targetFloor - currentFloor) * 0.05f, 0);
            transform.Translate(0, (targetFloor - currentFloor) * 0.05f, 0);
        }

        if (Mathf.Abs(transform.position.y - floor2y[targetFloor]) < 0.01f)
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
            move = false;
        }
    }

    public void restart(bool flag)
    {
        if (actFlag)
        {
            actFlag = false;
            if (flag)
            {
                _restart();
            }
            else
            {
                Invoke("_restart", 1.0f);
            }
        }
    }

    void _restart()
    {
        if (!door.activeSelf) door.SetActive(true);
        move = true;
        actFlag = true;
    }

    public void doorOpen()
    {
        if (door.activeSelf) door.SetActive(false);
    }
}