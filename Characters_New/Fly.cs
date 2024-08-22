using UnityEngine;

public class CharacterFloatControl : MonoBehaviour
{
    public float riseSpeed = 5f;  // 上升速度
    public float fallSpeed = 2.5f;  // 下落速度

    private Rigidbody rb;
    private bool isRising = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 确保重力作用被启用
        rb.useGravity = false;
    }

    void Update()
    {
        // 检测是否按住 V 键
        if (Input.GetKey(KeyCode.V))
        {
            isRising = true;
        }
        else
        {
            isRising = false;
        }
    }

    void FixedUpdate()
    {
        if (isRising)
        {
            // 持续上升
            rb.velocity = new Vector3(rb.velocity.x, riseSpeed, rb.velocity.z);
        }
        else
        {
            // 模拟下落：启用重力影响
            rb.velocity = new Vector3(rb.velocity.x, -fallSpeed, rb.velocity.z);
        }
    }
}
