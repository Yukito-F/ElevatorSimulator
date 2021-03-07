using UnityEngine;

public class Movetest : MonoBehaviour
{
    public int targetFloor = 2;
    public int currentFloor = 1;
    public int FloorMax = 3;

    float[] convert = { 0, 2, 5, 8 };

    [System.Serializable]
    public class Gain
    {
        public float p = 0.01f;
        public float i = 0.0f;
        public float d = 0.0f;
    }

    [SerializeField]
    protected Gain m_posGain = new Gain();

    private Rigidbody rb_;

    public float error_ = 0.0f;
    public float prevError_ = 0.0f;
    public float diffError_ = 0.0f;
    public float intError_ = 0.0f;

    public Vector3 force_ = Vector3.zero;

    public bool move = true;
    bool up = true;

    void Awake()
    {
        rb_ = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        error_ = convert[targetFloor] - transform.position.y;
        intError_ += error_ * Time.deltaTime; // integral
        diffError_ = (error_ - prevError_) / Time.deltaTime;
        prevError_ = error_;

        force_.y =
            m_posGain.p * error_ +
            m_posGain.i * intError_ +
            m_posGain.d * diffError_;


        if (move) rb_.AddForce(force_);

        if (Mathf.Abs(error_) < 0.02f)
        {
            move = false;
            rb_.velocity = Vector3.zero;
            currentFloor = targetFloor;
            if (up)
            {
                if (targetFloor < FloorMax)
                {
                    targetFloor++;
                }
                else
                {
                    up = false;
                    targetFloor--;
                }
            }
            else
            {
                if (targetFloor > 1)
                {
                    targetFloor--;
                }
                else
                {
                    up = true;
                    targetFloor++;
                }
            }
        }
    }
}