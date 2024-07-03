﻿using UnityEngine;
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
                //FPSText.color = Color.Lerp(Color.red, Color.green, GetFrameRate() * 0.16667f);  // 1 / 60.0f
                FPSText.color = Color.Lerp(Color.red, Color.green, GetFrameRate());
            }
        }

        private float deltaTime = 0.0f;

        /// <summary>
        /// Returns the value of the FPS(Frames per second) at the time it is called
        /// </summary>
        /// <returns></returns>
        public int GetFrameRate()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            int fps = (int)(1.0f / deltaTime);
            return fps;
        }
    }
}