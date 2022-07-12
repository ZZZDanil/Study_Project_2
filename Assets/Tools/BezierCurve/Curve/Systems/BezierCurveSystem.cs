using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BezierCurveSystem 
{
    public static void InitFirstPoint(BezierCurve bezierCurve)
    {
        if (bezierCurve != null)
        {
            bezierCurve.bezierPoints = new List<BezierPoint>();
            BezierCurveLoadPointSystem.AddPoint(bezierCurve, bezierCurve.transform.position);
            
        }
    }

    public static int FindBezierPointIndex(BezierCurve bezierCurve, BezierPoint bezierPoint)
    {
        for (int i = 0; i < bezierCurve.bezierPoints.Count; i++)
        {
            if (bezierCurve.bezierPoints[i].GetInstanceID() == bezierPoint.GetInstanceID())
            {
                return i;
            }
        }
        return -1;
    }
    public static bool IsBezierPointExistInCurve(BezierCurve bezierCurve, BezierPoint bezierPoint)
    {
        if (FindBezierPointIndex(bezierCurve, bezierPoint) > -1)
        {
            return true;
        }
        return false;
    }
    public static void ChangeLinkOnObject(BezierCurve bezierCurve, BezierPoint newBezierPoint, int oldInstanceID)
    {
        for (int i = 0; i < bezierCurve.bezierPoints.Count; i++)
        {
            if (bezierCurve.bezierPoints[i].GetInstanceID() == oldInstanceID)
            {
                bezierCurve.bezierPoints[i] = newBezierPoint;
            }
        }
    }
    public static void DeleteCurve(BezierCurve bezierCurve)
    {
        if (bezierCurve != null && bezierCurve.bezierPoints != null)
        {
            for (int i = 0; i < bezierCurve.bezierPoints.Count; i++)
            {
                if(bezierCurve.bezierPoints[i] != null)
                {
                    bezierCurve.bezierPoints[i].isCanDestroyed = true;
                }
                
            }
        }
    }
}
