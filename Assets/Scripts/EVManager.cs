using System.Collections.Generic;
using UnityEngine;

// エレベーターを待ってる人を管理するスクリプト
public class EVManager : MonoBehaviour
{
    // 待機者の情報を保持する構造体
    public struct StandbyInfo
    {
        // メンバ
        public User user { get; }
        public int currentFloor { get; }
        public bool up { get; }

        // コンストラクタ
        public StandbyInfo(GameObject obj)
        {
            user = obj.GetComponent<User>();
            currentFloor = user.curremtFloor;
            up = user.targetFloor > user.curremtFloor;
        }
    }

    public List<StandbyInfo> standbyList = new List<StandbyInfo>();

    // 待機エリアに入った時の処理
    private void OnTriggerEnter(Collider other)
    {
        User temp = other.GetComponent<User>();
        if (temp.targetObj == this.gameObject)
        {
            // 待機リストへの追加
            standbyList.Add(new StandbyInfo(other.gameObject));
        }
    }

    // 待機者リストからの取り出し
    public User standbyPop(int currentFloor, bool up)
    {
        StandbyInfo temp = standbyList.Find(x => x.currentFloor == currentFloor && x.up == up);
        if(temp.user != null) standbyList.Remove(temp);
        return temp.user;
    }
}
