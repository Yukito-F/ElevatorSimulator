using System;
using UnityEngine;

// 乗客のスクリプト
public class User : MonoBehaviour
{
    public int targetFloor;
    public int curremtFloor = 1;

    public GameObject targetObj;

    float[] floor2y = { 0, 2, 5, 8, 11, 14 };

    void Start()
    {
        // 初期の目標階の設定
        statusUpdate();
    }

    void Update()
    {
        Vector3 target = targetObj.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
        transform.Translate(Vector3.forward * 0.05f);


        if (transform.position.y < 0)
        {
            Vector3 temp = transform.position;
            temp.y = 20;
            transform.position = temp;
            Debug.Log("Fall");
        }
    }

    // 目標地点の更新
    public void shiftTarget(GameObject obj)
    {
        targetObj = obj;
    }

    // 目標階の更新、それに応じた色の変更
    public void statusUpdate()
    {
        curremtFloor = Array.IndexOf(floor2y, Mathf.RoundToInt(transform.position.y));

        do
        {
            targetFloor = UnityEngine.Random.Range(1, 6);
        }
        while (curremtFloor == targetFloor);

        switch (targetFloor)
        {
            case 1:
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 3:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case 4:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case 5:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            default:
                GetComponent<Renderer>().material.color = Color.black;
                break;
        }
    }
}