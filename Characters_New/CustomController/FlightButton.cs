using UnityEngine;
using UnityEngine.EventSystems;

public class FlightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerFlightControllerVelocity playerController; // 引用基于速度的控制器

    // 当按钮被按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerController != null)
        {
            playerController.StartThrust();
        }
    }

    // 当按钮被释放
    public void OnPointerUp(PointerEventData eventData)
    {
        if (playerController != null)
        {
            playerController.StopThrust();
        }
    }
}
