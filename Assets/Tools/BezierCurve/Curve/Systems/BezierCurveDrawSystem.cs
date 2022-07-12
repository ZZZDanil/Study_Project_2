using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurveDrawSystem
{
    public static void DrawBezier(BezierCurve bezierCurve, int quality = 10)
    {
        if (bezierCurve != null && bezierCurve.bezierPoints.Count > 1)
        {
            for (int i = 0; i < bezierCurve.bezierPoints.Count-1; i++)
            {
                /*Vector3 newPoint = BezierCurveMathSystem.DoBezier(
                        bezierCurve.bezierPoints[i], bezierCurve.bezierPoints[i + 1], 0.3f);*/

                Vector3 start = bezierCurve.bezierPoints[i].point;
                for (int q = 0; q < quality; q++)
                {
                    //Debug.Log($"DrawBezier count: {bezierCurve.bezierPoints.Count}; i: {i}; q: {q}; q+1: {q + 1}");
                    Vector3 newPoint = BezierCurveMathSystem.DoBezier(
                        bezierCurve.bezierPoints[i], bezierCurve.bezierPoints[i + 1], (float)q / quality);
                    UnityEditor.Handles.DrawLine(start, newPoint, 0.1f);
                    start = newPoint;
                }
                UnityEditor.Handles.DrawLine(start, bezierCurve.bezierPoints[i + 1].point, 0.1f);
            }
        }
    }
}

