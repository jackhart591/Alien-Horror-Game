using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pixelate_Camera : MonoBehaviour {

    public enum PixelScreenMode { Resize, Scale }

    [System.Serializable]
    public struct ScreenSize {
        public int width;
        public int height;
    }

    [Header("Screen Scaling Settings")]
    public PixelScreenMode mode;
    public ScreenSize targetScreenSize = new ScreenSize { width = 256, height = 144 };
    public uint screenScaleFactor = 1; 

    private Camera renderCamera;
    private RenderTexture renderTex;
    private int screenWidth, screenHeight;

    [Header("Display")]
    public RawImage display;
    public bool pixelated;

    public void Init() {
            // Initialize camera and get screen size values
            if (!renderCamera) renderCamera = GetComponent<Camera>();
            screenWidth = Screen.width;
            screenHeight = Screen.height;

            // prevent any errors
            if (screenScaleFactor < 1) screenScaleFactor = 1;
            if (targetScreenSize.width < 1) targetScreenSize.width = 1;
            if (targetScreenSize.height < 1) targetScreenSize.height = 1;

            int width;
            int height;

            if (pixelated) {
                // Calculate the render texture size
                width = mode == PixelScreenMode.Resize ? (int)targetScreenSize.width : screenWidth / (int)screenScaleFactor;
                height = mode == PixelScreenMode.Resize ? (int)targetScreenSize.height : screenHeight / (int)screenScaleFactor;
            } else {
                width = screenWidth;
                height = screenHeight;
            }

        // Init render tex
        renderTex = new RenderTexture(width, height, 24) {
            filterMode = FilterMode.Point,
            antiAliasing = 1
        };

        // Set the render tex as camera's output
        renderCamera.targetTexture = renderTex;

        // Attach texture to the display UI Rawimage
        display.texture = renderTex;
    }

    public bool CheckScreenResize() {
        // Check whether the screen has been resized
        return Screen.width != screenWidth || Screen.height != screenHeight;
    }

    void Start() {
        Init();
    }

    void Update() {
        if (CheckScreenResize()) Init();
    }
}