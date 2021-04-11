using UnityEngine;

public class Task : MonoBehaviour
{
    GameObject evManager;
    public int count = 0;

    void Start()
    {
        evManager = GameObject.Find("EVManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        User temp = other.GetComponent<User>();
        if (temp.targetObj == this.gameObject)
        {
            temp.statusUpdate();
            temp.shiftTarget(evManager);

            count++;
        }
    }
}
