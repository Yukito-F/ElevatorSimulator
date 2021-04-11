using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    GameObject user;
    public int maxUser;

    float[] floor2y = { 0, 2, 5, 8, 11, 14 };

    private void Start()
    {
        user = (GameObject)Resources.Load("User");
        for (int i = 0; i < maxUser; i++)
        {
            GameObject userClone = Instantiate(user, new Vector3(0, floor2y[1], -8 - i), Quaternion.identity);
            User _userClone = userClone.GetComponent<User>();
            _userClone.shiftTarget(GameObject.Find("EVManager"));
        }
    }
}
