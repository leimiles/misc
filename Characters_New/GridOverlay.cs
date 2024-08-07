using UnityEngine;

public class GridOverlay : MonoBehaviour
{
    public int rows = 7;
    public int columns = 12;
    public Color lineColor = Color.white;
    public float lineWidth = 4.0f; // 线条宽度

    void OnGUI()
    {
        DrawGrid();
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
