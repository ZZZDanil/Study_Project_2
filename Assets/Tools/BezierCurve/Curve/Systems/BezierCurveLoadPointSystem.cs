using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BezierCurveLoadPointSystem
{
    private static Object newPoint;
    const string pathToBezierPoint = "Assets/Tools/BezierCurve/Point/BezierPoint.prefab";
    public static void AddPoint(BezierCurve bezierCurve, Vector3 newPointPosition)
    {
        BezierPoint newBezierPoint = LoadNewPoint(bezierCurve, newPointPosition);
        if (newBezierPoint != null)
        {
            bezierCurve.bezierPoints.Add(newBezierPoint);
            Selection.activeObject = newBezierPoint ;
        }
    }
    public static void AddPointToFirst(BezierCurve bezierCurve, Vector3 newPointPosition)
    {
        BezierPoint newBezierPoint = LoadNewPoint(bezierCurve, newPointPosition);
        if (newBezierPoint != null)
        {
            bezierCurve.bezierPoints.Insert(0, newBezierPoint);
            Selection.activeObject = newBezierPoint;
        }
    }

    private static BezierPoint LoadNewPoint(BezierCurve bezierCurve, Vector3 newPointPosition)
    {
        if (bezierCurve.bezierPoints != null)
        {
            if (newPoint == null)
            {
                newPoint = (Object)AssetDatabase.LoadAssetAtPath(pathToBezierPoint, typeof(Object));
            }
            GameObject newGameObject = (GameObject)GameObject.Instantiate(newPoint, newPointPosition, Quaternion.identity, bezierCurve.transform);
            if (newGameObject != null)
            {
                newGameObject.name = "Point";
                BezierPoint newBezierPoint = newGameObject.GetComponent<BezierPoint>();
                if (newBezierPoint != null)
                {
                    newBezierPoint.isCanDestroyed = false;
                }
                return newBezierPoint;
            }
        }
        return null;
    }
}
