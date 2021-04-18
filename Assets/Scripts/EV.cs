using System.Collections.Generic;
using UnityEngine;

// エレベーター本体のスクリプト
public class EV : MonoBehaviour
{
    // 乗客の情報を保持する構造体
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

    GameObject evManager;
    EVManager evManager_s;
    GameObject taskArea;

    public int capacity = 10;
    int count;

    int targetFloor = 2;
    public int currentFloor = 1;
    int FloorMax = 5;

    float[] floor2y = { 0, 2, 5, 8, 11, 14 };

    public bool move = true;
    public bool up = true;
    bool actFlag = true;

    GameObject door;

    bool openFlag = true;

    // 各種ゲームオブジェクトの取得
    void Start()
    {
        evManager = GameObject.Find("EVManager");
        evManager_s = evManager.GetComponent<EVManager>();
        taskArea = GameObject.Find("TaskArea");
        door = transform.Find("door").gameObject;
    }

    void Update()
    {
        if (move)
        {
            // 固定：マイフレーム0.05fずつ上下に移動
            transform.Translate(0, (targetFloor - currentFloor) * 0.05f, 0);

            // 目標階に到達次第停止、進行方向の更新
            if (Mathf.Abs(transform.position.y - floor2y[targetFloor]) < 0.01f)
            {
                currentFloor = targetFloor;
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
                move = false;
            }
        }
        else
        {
            // 乗車待機リストに人がいるときの処理
            if (evManager_s.standbyList.Count > 0 && count < capacity)
            {
                User temp = evManager_s.standbyPop(currentFloor, up);
                if (temp != null)
                {
                    bufferList.Add(new RideInfo(temp));
                    temp.shiftTarget(this.gameObject);
                    count++;
                }
            }

            // 乗車リストに人がいるときの処理
            if (riderList.Count > 0)
            {
                RideInfo tempUser = riderList.Find(x => x.targetFloor == currentFloor);
                if (tempUser.user != null)
                {
                    riderList.Remove(tempUser);
                    bufferList.Add(tempUser);
                    tempUser.user.shiftTarget(taskArea);
                    count--;
                }
            }

            // 処理中のリストに人がいるときの処理
            if (bufferList.Count > 0)
            {
                // 処理中の人がいるときはドアを開ける
                doorOpen();
                openFlag = false;
            }
            else
            {
                // いなければリスタート
                restart(openFlag);
                openFlag = true;
            }
        }
    }

    // 乗車時の処理
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

    // 降車時の処理
    private void OnTriggerExit(Collider other)
    {
        User temp = other.GetComponent<User>();
        if (temp.targetObj == taskArea)
        {
            RideInfo tempUser = bufferList.Find(x => x.user == temp);
            bufferList.Remove(tempUser);
        }
    }

    // 再発進の処理
    public void restart(bool flag)
    {
        if (actFlag)
        {
            actFlag = false;
            if (flag)
            {
                _restart();
            }
            else
            {
                Invoke("_restart", 1.0f);
            }
        }
    }

    void _restart()
    {
        if (!door.activeSelf) door.SetActive(true);
        move = true;
        actFlag = true;
    }

    public void doorOpen()
    {
        if (door.activeSelf) door.SetActive(false);
    }
}
