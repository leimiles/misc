using UnityEngine;
using UnityEngine.UI; // 引入 UI 命名空间

public class GridOverlay : MonoBehaviour
{
    public int rows = 3;
    public int columns = 3;
    public Color lineColor = Color.white;
    public float lineWidth = 2.0f; // 线条宽度
    private bool showGrid = false; // 控制网格显示与隐藏的布尔值

    public Button toggleGridButton; // 引用按钮
    public Text buttonText; // 引用按钮的文字

    void Start()
    {
        // 确保按钮和文字引用正确
        if (toggleGridButton != null)
        {
            toggleGridButton.onClick.AddListener(ToggleGrid);
        }

        UpdateButtonText(); // 初始化按钮文字
    }

    void Update()
    {
        // 检查快捷键输入
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleGrid();
        }
    }

    void ToggleGrid()
    {
        showGrid = !showGrid;
        UpdateButtonText(); // 更新按钮文字
    }

    void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = showGrid ? "隐藏九宫格" : "显示九宫格";
        }
    }

    void OnGUI()
    {
        if (showGrid)
        {
            DrawGrid();
        }
    }

    void DrawGrid()
    {
        float width = Screen.width;
        float height = Screen.height;

        // 设置线条颜色
        Color oldColor = GUI.color;
        GUI.color = lineColor;

        // 绘制行
        for (int i = 1; i < rows; i++)
        {
            float y = height / rows * i;
            DrawLine(new Vector2(0, y), new Vector2(width, y), lineWidth);
        }

        // 绘制列
        for (int i = 1; i < columns; i++)
        {
            float x = width / columns * i;
            DrawLine(new Vector2(x, 0), new Vector2(x, height), lineWidth);
        }

        // 恢复原来的颜色
        GUI.color = oldColor;
    }

    void DrawLine(Vector2 pointA, Vector2 pointB, float width)
    {
        Matrix4x4 matrix = GUI.matrix;
        float angle = Vector3.Angle(pointB - pointA, Vector2.right);
        if (pointA.y > pointB.y) angle = -angle;
        GUIUtility.RotateAroundPivot(angle, pointA);
        GUI.DrawTexture(new Rect(pointA.x, pointA.y - width / 2, (pointB - pointA).magnitude, width), Texture2D.whiteTexture);
        GUI.matrix = matrix;
    }
}
