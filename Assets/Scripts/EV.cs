﻿using System.Collections;
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
            // 乗車待機リストに人がいれば
            if (temp != null)
            {
                rideStandbyList.Add(new RideInfo(temp));
                temp.shiftTarget(this.gameObject);
            }

            if (riderList.Count != 0)
            {
                RideInfo tempUser = riderList.Find(x => x.targetFloor == evMove.currentFloor);
                rideStandbyList.Add(tempUser);
                if (!ReferenceEquals(tempUser.user, null))
                    tempUser.user.shiftTarget(evManager);
            }

            if (rideStandbyList.Count == 0) evMove.move = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        User temp = other.GetComponent<User>();
        RideInfo tempUser = rideStandbyList.Find(x => x.user == temp);
        rideStandbyList.Remove(tempUser);
        riderList.Add(tempUser);
    }

    private void OnTriggerExit(Collider other)
    {
        User temp = other.GetComponent<User>();
        RideInfo tempUser = riderList.Find(x => x.user == temp);
        riderList.Remove(tempUser);
    }

}
