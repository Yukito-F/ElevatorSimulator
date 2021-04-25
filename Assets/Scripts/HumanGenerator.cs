using UnityEngine;

// 最初に一定人数の乗客を生成するスクリプト
public class HumanGenerator : MonoBehaviour
{
    GameObject user;
    public int maxUser;
    public bool random;
    int rangeMax = 1;

    float[] floor2y = { 0, 2, 5, 8, 11, 14 };

    private void Start()
    {
        user = (GameObject)Resources.Load("User");
        if (random) rangeMax = 6;
        for (int i = 0; i < maxUser; i++)
        {
            GameObject userClone = Instantiate(user, new Vector3(0, floor2y[Random.Range(1, rangeMax)], -8 - i), Quaternion.identity);
            User _userClone = userClone.GetComponent<User>();
            _userClone.shiftTarget(GameObject.Find("EVManager"));
        }
    }
}
