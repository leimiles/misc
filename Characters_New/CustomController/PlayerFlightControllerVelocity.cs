using UnityEngine;

public class PlayerFlightControllerVelocity : MonoBehaviour
{
    [Header("飞行参数")]
    public float upwardSpeed = 5f;     // 角色上升时的速度
    public float downwardSpeed = 10f;  // 角色下落时的最大速度
    public float maxHeight = 50f;       // 角色的最大飞行高度

    private Rigidbody rb;
    private bool isThrusting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody组件未找到！");
        }
    }

    void Update()
    {
        // 检测键盘V键的按下和释放
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartThrust();
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            StopThrust();
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;

        // 检查当前高度是否达到最大高度
        if (transform.position.y >= maxHeight)
        {
            // 阻止进一步上升
            if (velocity.y > 0)
            {
                velocity.y = 0;
            }
        }
        else
        {
            if (isThrusting)
            {
                // 设置角色的垂直速度为上升速度
                velocity.y = upwardSpeed;
            }
            else
            {
                // 如果角色正在下落，限制其下落速度
                if (velocity.y < -downwardSpeed)
                {
                    velocity.y = -downwardSpeed;
                }
                // 否则，允许重力自然作用
            }
        }

        rb.velocity = velocity;
    }

    // 开始飞行
    public void StartThrust()
    {
        isThrusting = true;
    }

    // 停止飞行
    public void StopThrust()
    {
        isThrusting = false;
    }
}
