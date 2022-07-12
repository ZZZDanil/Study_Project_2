using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurveMathSystem 
{
    public static Vector3 DoBezier(BezierPoint from, BezierPoint to, float position)
    {
        Vector3 centerPos = Vector3.Lerp(from.manipulatorAfter + from.point, to.manipulatorBefore + to.point, position);
        Vector3 fromPos = Vector3.Lerp(from.point, from.manipulatorAfter + from.point, position);
        Vector3 toPos = Vector3.Lerp(to.manipulatorBefore + to.point, to.point, position);
        
        Vector3 fromBezier = Vector3.Lerp(fromPos, centerPos, position);
        Vector3 toBezier = Vector3.Lerp(centerPos, toPos, position);

        /*UnityEditor.Handles.DrawLine(centerPos, fromPos, 0.1f);
        UnityEditor.Handles.DrawLine(centerPos, toPos, 0.1f);
        UnityEditor.Handles.DrawLine(fromPos, toPos, 0.1f);
        UnityEditor.Handles.DrawLine(fromBezier, toBezier, 0.1f);


        UnityEditor.Handles.DrawLine(Vector3.Lerp(fromBezier, toBezier, position), centerPos, 0.1f);*/

        //Debug.Log($"position: {Vector3.Lerp(fromBezier, toBezier, position)}");

        return Vector3.Lerp(fromBezier, toBezier, position);
    }
}
