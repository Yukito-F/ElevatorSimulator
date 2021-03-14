using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVManager : MonoBehaviour
{
    public struct StandbyInfo
    {
        public User user { get; }
        public int currentFloor { get; }
        public bool up { get; }

        public StandbyInfo(GameObject obj)
        {
            user = obj.GetComponent<User>();
            currentFloor = user.curremtFloor;
            up = user.targetFloor > user.curremtFloor;
        }
    }

    public List<StandbyInfo> standbyList = new List<StandbyInfo>();

    private void OnTriggerEnter(Collider other)
    {
        User temp = other.GetComponent<User>();
        temp.statusUpdate();
        standbyAdd(other.gameObject);
    }

    public void standbyAdd(GameObject user)
    {
        standbyList.Add(new StandbyInfo(user));
    }

    public User standbyPop(int currentFloor, bool up)
    {
        StandbyInfo temp = standbyList.Find(x => (x.currentFloor == currentFloor && x.up == up));
        standbyList.Remove(temp);
        return temp.user;
    }
}
