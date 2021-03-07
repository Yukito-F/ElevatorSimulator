using System.Collections;
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
    public List<RideInfo> rideStandbyList = new List<RideInfo>();

    ElevatorMove evMove;
    GameObject evManager;
    EVManager evManager_s;

    // Start is called before the first frame update
    void Start()
    {
        evMove = GetComponent<ElevatorMove>();
        evManager = GameObject.Find("EVManager");
        evManager_s = evManager.GetComponent<EVManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!evMove.move)
        {
            User temp = evManager_s.standbyPop(evMove.currentFloor, evMove.up);
            if (temp != null) rideStandbyList.Add(new RideInfo(temp));
            if (rideStandbyList.Count == 0) evMove.move = true;
        }
    }
}
