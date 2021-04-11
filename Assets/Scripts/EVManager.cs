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
        if (temp.targetObj == this.gameObject)
        {
            standbyList.Add(new StandbyInfo(other.gameObject));
        }
    }

    public User standbyPop(int currentFloor, bool up)
    {
        StandbyInfo temp = standbyList.Find(x => x.currentFloor == currentFloor && x.up == up);
        if(temp.user != null) standbyList.Remove(temp);
        return temp.user;
    }
}
