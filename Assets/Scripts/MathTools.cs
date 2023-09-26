using UnityEngine;

public class MathTools
{
    public static Vector3 GetPointInCircleFromCenter(Vector3 center, float angle, float radius)
    {
        float angleRadians = angle * Mathf.Deg2Rad;

        Vector3 circlePoint = center + new Vector3(
            Mathf.Cos(angleRadians) * radius,
            0.0f,
            Mathf.Sin(angleRadians) * radius
        );

        return circlePoint;
    }
}