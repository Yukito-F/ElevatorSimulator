using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public int targetFloor = 1;
    public int curremtFloor = 1;

    bool move = true;
    GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("EVManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Vector3 target = targetObj.transform.position;
            target.y = transform.position.y;
            transform.LookAt(target);
            transform.Translate(Vector3.forward * 0.02f);
        }
    }

    public void shiftTarget(GameObject obj)
    {
        targetObj = obj;
    }

    public void statusUpdate()
    {
        curremtFloor = targetFloor;

        do
        {
            targetFloor = Random.Range(1, 6);
        }
        while (curremtFloor == targetFloor);
    }
}