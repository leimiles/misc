// using UnityEngine;
// using TrailsFX;
// using Unity.Mathematics;

// public class DashAbility : MonoBehaviour
// {
//     public float dashDistance = 10f; // 冲刺距离
//     public float dashSpeed = 20f; // 冲刺速度
//     private bool isDashing = false;
//     private Vector3 dashDirection;
//     private float remainingDistance;
//     public float MaxAngel = 15f;

//     private Rigidbody rb;
//     public TrailEffect trailEffect;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         if (rb == null)
//         {
//             Debug.LogError("Rigidbody component is missing on the GameObject.");
//         }
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.F) && !isDashing)
//         {
//             StartDash();
//         }
//     }

//     void FixedUpdate()
//     {
//         if (isDashing)
//         {
//             ContinueDash();
//         }
//     }

//     void StartDash()
//     {
//         isDashing = true;
//         dashDirection = transform.forward;
//         remainingDistance = dashDistance;
//         if (trailEffect != null)
//         {
//             trailEffect.active = true;
//         }
//     }

//     void ContinueDash()
//     {
//         if (remainingDistance > 0f)
//         {
//             float dashStep = dashSpeed * Time.fixedDeltaTime;

//             if (Physics.Raycast(transform.position + transform.forward + transform.up * 2f, Vector3.down, out RaycastHit groundHit, 10f))
//             {
//                 var normal = groundHit.normal;
//                 var right = math.cross(normal, dashDirection);
//                 dashDirection = math.cross(right, normal);
//             }

//             Vector3 move = dashDirection * dashStep;

//             rb.MovePosition(rb.position + move);
//             remainingDistance -= dashStep;

//         }
//         else
//         {
//             StopDash();
//         }
//     }

//     void StopDash()
//     {
//         isDashing = false;

//         if (trailEffect != null)
//         {
//             trailEffect.active = false;
//             // a.enableEffect = false; // 禁用轨迹效果
//         }
//     }
// }


// V2 带缓动效果
using UnityEngine;
using TrailsFX;
using Unity.Mathematics;
using UnityEngine.UI;

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 10f; // 冲刺距离
    public float dashSpeed = 20f; // 最大冲刺速度
    private bool isDashing = false;
    private Vector3 dashDirection;
    private float initialDistance;
    private float remainingDistance;

    private Rigidbody rb;
    public TrailEffect trailEffect;
    public AudioClip dashSound; // 冲刺音效
    private AudioSource audioSource;

    [Header("Easing Curve")]
    public AnimationCurve easingCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    [Header("Camera Settings")]
    public Camera mainCamera;
    public float fovChangeAmount = 10f; // FOV 变化量
    public float fovChangeSpeed = 5f; // FOV 变化速度
    private float originalFov;

    public Button dashButton;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the GameObject.");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            originalFov = mainCamera.fieldOfView;
        }
        else
        {
            Debug.LogError("Main camera is not assigned and no Camera tagged as MainCamera.");
        }
        if (dashButton != null)
        {
            dashButton.onClick.AddListener(StartDash);
        }
                audioSource = GetComponent<AudioSource>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the GameObject.");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isDashing)
        {
            StartDash();
        }

        if (!isDashing && mainCamera != null)
        {
            // 恢复原始 FOV
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, originalFov, Time.deltaTime * fovChangeSpeed);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            ContinueDash();
        }

        // Debug.Log("isdashing:" + isDashing);
        // Debug.Log("remainingDistance:" + remainingDistance);
    }

    void StartDash()
    {
        isDashing = true;
        dashDirection = transform.forward;
        remainingDistance = dashDistance;
        initialDistance = dashDistance;

        if (trailEffect != null)
        {
            trailEffect.active = true;
        }
        PlayDashSound();
    }

    void ContinueDash()
    {
        if (remainingDistance > 0f)
        {
            float distanceCovered = initialDistance - remainingDistance;
            float t = distanceCovered / initialDistance; // 从0到1的归一化时间

            // 使用缓动曲线来计算当前速度
            float currentSpeed = dashSpeed * easingCurve.Evaluate(t);
            float dashStep = currentSpeed * Time.fixedDeltaTime;

            if (Physics.Raycast(transform.position + transform.forward + transform.up * 2f, Vector3.down, out RaycastHit groundHit, 10f))
            {
                var normal = groundHit.normal;
                var right = math.cross(normal, dashDirection);
                dashDirection = math.cross(right, normal);
            }

            Vector3 move = dashDirection * dashStep;

            rb.MovePosition(rb.position + move);
            remainingDistance -= dashStep;

            // 改变相机 FOV
            if (mainCamera != null)
            {
                float targetFov = originalFov - fovChangeAmount;
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFov, Time.fixedDeltaTime * fovChangeSpeed);
            }
        }
        else
        {
            StopDash();
        }
    }

    void StopDash()
    {
        isDashing = false;

        if (trailEffect != null)
        {
            trailEffect.active = false;
        }
    }
    void PlayDashSound()
    {
        if (audioSource != null && dashSound != null)
        {
            audioSource.PlayOneShot(dashSound);
        }
    }
}
