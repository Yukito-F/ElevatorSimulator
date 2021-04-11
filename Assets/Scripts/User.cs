using UnityEngine;

public class User : MonoBehaviour
{
    public int targetFloor;
    public int curremtFloor = 1;

    bool move = false;
    public GameObject targetObj;

    void Start()
    {
        statusUpdate();
    }

    void Update()
    {
        if (move)
        {
            Vector3 target = targetObj.transform.position;
            target.y = transform.position.y;
            transform.LookAt(target);
            transform.Translate(Vector3.forward * 0.05f);
        }

        if (transform.position.y < 0)
        {
            Vector3 temp = transform.position;
            temp.y = 20;
            transform.position = temp;
            Debug.Log("Fall");
        }
    }

    public void shiftTarget(GameObject obj)
    {
        targetObj = obj;
        move = true;
    }

    public void stopMove()
    {
        move = false;
    }

    public void statusUpdate()
    {
        curremtFloor = targetFloor;

        do
        {
            targetFloor = Random.Range(1, 6);
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

        move = true;
    }
}