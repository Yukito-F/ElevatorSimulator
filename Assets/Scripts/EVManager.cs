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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void standbyAdd(GameObject user)
    {
        standbyList.Add(new StandbyInfo(user));
    }

    public User standbyPop(int currentFloor, bool up)
    {
        StandbyInfo temp =  standbyList.Find(x => (x.currentFloor == currentFloor && x.up == up));
        if (!ReferenceEquals(temp, null))
        {
            standbyList.Remove(temp);
            return temp.user;
        }
        else
        {
            return null;
        }
    }
}
