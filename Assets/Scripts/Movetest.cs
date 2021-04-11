using UnityEngine;

public class Movetest : MonoBehaviour
{
    int targetFloor = 2;
    public int currentFloor = 1;
    int FloorMax = 5;

    float[] convert = { 0, 2, 5, 8, 11, 14 };

    public bool move = true;
    public bool up = true;


    void Update()
    {
        if (move) transform.Translate(0, (convert[targetFloor] - transform.position.y)/Mathf.Abs(convert[targetFloor] - transform.position.y) * 0.5f, 0);

        if (Mathf.Abs(transform.position.y - convert[targetFloor]) < 0.01f)
        {
            currentFloor = targetFloor;
            move = false;
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