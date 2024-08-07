// using UnityEngine;
// using System.Collections;

// public class TeleportManager : MonoBehaviour
// {
//     public Transform TargetArea; // 区域 B 的位置
//     private Transform player; // 玩家对象
//     public Vector3 Offset;
//     public float teleportSpeed = 5f; // 传送速度

//     private void OnTriggerEnter(Collider other)
//     {
//         // Debug.Log("OnTriggerEnter called with " + other.name); // 调试日志

//         if (other.CompareTag("Player"))
//         {
//             player = other.transform;
//             StartCoroutine(TeleportTo(TargetArea.position));
//         }
//         else
//         {
//             // Debug.Log("Object entering trigger is not the player.");
//         }
//     }

//     private IEnumerator TeleportTo(Vector3 targetPosition)
//     {
//         // Debug.Log("Teleporting to " + targetPosition); // 调试日志

//         if (player != null)
//         {
//             Vector3 startPosition = player.position;
//             Vector3 endPosition = targetPosition + Offset;
//             float elapsedTime = 0f;

//             while (elapsedTime < 1f)
//             {
//                 player.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
//                 elapsedTime += Time.deltaTime * teleportSpeed;
//                 yield return null;
//             }

//             player.position = endPosition; // 确保最终位置准确
//             // Debug.Log("Teleport successful");
//         }
//         else
//         {
//             // Debug.LogError("Player reference is null.");
//         }
//     }
// }


//v2 抛物线方式
using UnityEngine;
using System.Collections;

public class TeleportManager : MonoBehaviour
{
    public Transform TargetArea; // 区域 B 的位置
    private Transform player; // 玩家对象
    public Vector3 Offset;
    public float teleportSpeed = 5f; // 传送速度
    public float height = 5f; // 抛物线的高度

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("OnTriggerEnter called with " + other.name); // 调试日志

        if (other.CompareTag("Player"))
        {
            player = other.transform;
            StartCoroutine(TeleportTo(TargetArea.position));
        }
        else
        {
            // Debug.Log("Object entering trigger is not the player.");
        }
    }

    private IEnumerator TeleportTo(Vector3 targetPosition)
    {
        // Debug.Log("Teleporting to " + targetPosition); // 调试日志

        if (player != null)
        {
            Vector3 startPosition = player.position;
            Vector3 endPosition = targetPosition + Offset;
            Vector3 controlPoint = (startPosition + endPosition) / 2 + Vector3.up * height;
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                float t = elapsedTime * teleportSpeed;
                player.position = CalculateBezierPoint(t, startPosition, controlPoint, endPosition);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            player.position = endPosition; // 确保最终位置准确
            // Debug.Log("Teleport successful");
        }
        else
        {
            // Debug.LogError("Player reference is null.");
        }
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * p0
        p += 2 * u * t * p1; // 2(1-t)t * p1
        p += tt * p2; // t^2 * p2

        return p;
    }
}

// //抛物线带特效
// using UnityEngine;
// using System.Collections;

// public class TeleportManager : MonoBehaviour
// {
//     public Transform TargetArea; // 区域 B 的位置
//     private Transform player; // 玩家对象
//     public Vector3 Offset;
//     public float teleportSpeed = 1f; // 传送速度
//     public float height = 500f; // 抛物线的高度
//     public GameObject teleportEffect; // 传送特效

//     private void OnTriggerEnter(Collider other)
//     {
//         // Debug.Log("OnTriggerEnter called with " + other.name); // 调试日志

//         if (other.CompareTag("Player"))
//         {
//             player = other.transform;
//             StartCoroutine(TeleportTo(TargetArea.position));
//         }
//         else
//         {
//             // Debug.Log("Object entering trigger is not the player.");
//         }
//     }

//     private IEnumerator TeleportTo(Vector3 targetPosition)
//     {
//         // Debug.Log("Teleporting to " + targetPosition); // 调试日志

//         if (player != null)
//         {
//             Vector3 startPosition = player.position;
//             Vector3 endPosition = targetPosition + Offset;
//             Vector3 controlPoint = (startPosition + endPosition) / 2 + Vector3.up * height;
//             float elapsedTime = 0f;

//             // 启用特效
//             if (teleportEffect != null && !teleportEffect.activeSelf)
//             {
//                 teleportEffect.SetActive(true);
//             }

//             while (elapsedTime < 1f)
//             {
//                 float t = elapsedTime * teleportSpeed;
//                 player.position = CalculateBezierPoint(t, startPosition, controlPoint, endPosition);
//                 elapsedTime += Time.deltaTime;
//                 yield return null;
//             }

//             player.position = endPosition; // 确保最终位置准确

//             // 禁用特效
//             if (teleportEffect != null && teleportEffect.activeSelf)
//             {
//                 teleportEffect.SetActive(false);
//             }

//             // Debug.Log("Teleport successful");
//         }
//         else
//         {
//             // Debug.LogError("Player reference is null.");
//         }
//     }

//     private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
//     {
//         float u = 1 - t;
//         float tt = t * t;
//         float uu = u * u;

//         Vector3 p = uu * p0; // (1-t)^2 * p0
//         p += 2 * u * t * p1; // 2(1-t)t * p1
//         p += tt * p2; // t^2 * p2

//         return p;
//     }
// }

