using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BezierPointSystem
{
    public static bool IsInPefabMode(GameObject g)
    {
        if (g != null)
        {
            bool b = (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null);
            return b;
        }
        else
        {
            return false;
        }
    }
    public static bool IsInPefabMode(BezierPoint bezierPoint)
    {
        if (bezierPoint != null)
            return IsInPefabMode(bezierPoint.gameObject);
        else
        {
            return false;
        }
    }
    public static void FindBezierPointByNameAndDestroyExcess(BezierPoint bezierPoint, BezierCurve bezierCurve)
    {
        if (bezierPoint != null)
        {
            if (bezierCurve != null)
            {
                if (bezierCurve.bezierPoints != null)
                {

                    bool isExistInCurveArray = BezierCurveSystem.IsBezierPointExistInCurve(bezierCurve, bezierPoint);
                    if (isExistInCurveArray == false)
                    {
                        GameObject.DestroyImmediate(bezierPoint.gameObject);
                    }
                }
            }
            else
            {
                if(!IsInPefabMode(bezierPoint))
                    GameObject.DestroyImmediate(bezierPoint.gameObject);
            }
        }
    }
    public static int PointsPositionInBezier(BezierPoint bezierPoint, BezierCurve bezierCurve)
    {
        if (bezierPoint != null)
        {
            if (bezierCurve != null && bezierCurve.bezierPoints != null)
            {
                BezierPoint first = bezierCurve.bezierPoints[0];
                BezierPoint last = bezierCurve.bezierPoints[bezierCurve.bezierPoints.Count - 1];
                if (first != null && first.GetInstanceID() == bezierPoint.GetInstanceID())
                {
                    return -1;
                }
                if (last != null && last.GetInstanceID() == bezierPoint.GetInstanceID())
                {
                    return -1;
                }
            }
        }
        return 0;
    }
}