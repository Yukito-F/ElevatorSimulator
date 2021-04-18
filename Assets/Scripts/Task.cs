using UnityEngine;

// フロアの端に到達したら目標階を変更するスクリプト
public class Task : MonoBehaviour
{
    GameObject evManager;

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
        }   
    }
}
