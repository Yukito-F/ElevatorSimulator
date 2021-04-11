using System.Collections.Generic;
using UnityEngine;

public class EV : MonoBehaviour
{
    public struct RideInfo
    {
        public User user { get; }
        public int targetFloor { get; }
        public RideInfo(User obj)
        {
            user = obj;
            targetFloor = obj.targetFloor;
        }
    }

    public List<RideInfo> riderList = new List<RideInfo>();
    public List<RideInfo> bufferList = new List<RideInfo>();

    ElevatorMove evMove;
    GameObject evManager;
    EVManager evManager_s;
    GameObject taskArea;

    public int capacity = 10;
    int count;

    bool openFlag = true;

    void Start()
    {
        evMove = GetComponent<ElevatorMove>();
        evManager = GameObject.Find("EVManager");
        evManager_s = evManager.GetComponent<EVManager>();
        taskArea = GameObject.Find("TaskArea");
    }

    void Update()
    {
        if (!evMove.move)
        {
            // 乗車待機リストに人がいれば
            if (evManager_s.standbyList.Count > 0 && count < capacity)
            {
                User temp = evManager_s.standbyPop(evMove.currentFloor, evMove.up);
                if (temp != null)
                {
                    bufferList.Add(new RideInfo(temp));
                    temp.shiftTarget(this.gameObject);
                    count++;
                }
            }

            if (riderList.Count > 0)
            {
                RideInfo tempUser = riderList.Find(x => x.targetFloor == evMove.currentFloor);
                if (tempUser.user != null)
                {
                    riderList.Remove(tempUser);
                    bufferList.Add(tempUser);
                    tempUser.user.shiftTarget(taskArea);
                    count--;
                }
            }

            if (bufferList.Count > 0)
            {
                evMove.doorOpen();
                openFlag = false;
            }
            else
            {
                evMove.restart(openFlag);
                openFlag = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        User temp = other.GetComponent<User>();
        if (temp.targetObj == this.gameObject)
        {
            RideInfo tempUser = bufferList.Find(x => x.user == temp);
            riderList.Add(tempUser);
            bufferList.Remove(tempUser);
        }   
    }


    private void OnTriggerExit(Collider other)
    {
        User temp = other.GetComponent<User>();
        if (temp.targetObj == taskArea)
        {
            RideInfo tempUser = bufferList.Find(x => x.user == temp);
            bufferList.Remove(tempUser);
        }
    }
}
