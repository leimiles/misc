using UnityEngine;
using UnityEngine.UI;
namespace JUTPS.Utilities
{
    [AddComponentMenu("JU TPS/UI/FPS Counter")]
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private Text FPSText;
        public float RefreshRate;
        void Start()
        {
            InvokeRepeating("UpdateFrameRateOnScreen", 0, RefreshRate);

            //if that component does not have a text assigned, it will look locally for a text component.
            if (FPSText == null && GetComponent<Text>() != null) { FPSText = GetComponent<Text>(); }
        }
        public void UpdateFrameRateOnScreen()
        {
            if (FPSText != null)
            {
                FPSText.text = GetFrameRate().ToString();
                FPSText.color = Color.Lerp(Color.red, Color.green, GetFrameRate() * 0.16667f);  // 1 / 60.0f
            }
        }
        /// <summary>
        /// Returns the value of the FPS(Frames per second) at the time it is called
        /// </summary>
        /// <returns></returns>
        public static int GetFrameRate()
        {
            int fps = (int)(1.0f / Time.unscaledDeltaTime);
            return fps;
        }
    }
}