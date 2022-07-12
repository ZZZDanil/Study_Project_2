using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BezierPointDrawSystem 
{
    public static void DrawGUI(BezierPoint bezierPoint)
    {
        bezierPoint.point = bezierPoint.transform.position;

        EditorGUI.BeginChangeCheck();

        (Vector3 newManipulatorBefore, Vector3 newManipulatorAfter) = DrawHandles(bezierPoint);
        newManipulatorBefore -= bezierPoint.point;
        newManipulatorAfter -= bezierPoint.point;

        ChangeSingleStatus(bezierPoint);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeManipulatorsPositions(bezierPoint, newManipulatorBefore, newManipulatorAfter);
        }
    }

    public static (Vector3, Vector3) DrawHandles(BezierPoint bezierPoint)
    {
        Vector3 cameraPos = SceneView.lastActiveSceneView.camera.transform.position;

        Vector3 posBefore = bezierPoint.manipulatorBefore + bezierPoint.point;
        Vector3 posAfter = bezierPoint.manipulatorAfter + bezierPoint.point;
        //Debug.Log($"bezierPoint.point {bezierPoint.point}");
        Handles.DrawLine(bezierPoint.point, posBefore, 0.1f);
        Handles.DrawLine(bezierPoint.point, posAfter, 0.1f);

        Handles.color = Color.red;
        Handles.DrawSolidDisc(posBefore, posBefore - cameraPos, 0.1f);
        Handles.color = Color.green;
        Handles.DrawSolidDisc(posAfter, posAfter - cameraPos, 0.1f);

        Vector3 snap = Vector3.one * 0.0f;
        Handles.color = Color.red;
        Vector3 newManipulatorBefore = Handles.FreeMoveHandle(posBefore, Quaternion.identity, 0.1f, snap, Handles.CircleHandleCap);
        Handles.color = Color.green;
        Vector3 newManipulatorAfter = Handles.FreeMoveHandle(posAfter, Quaternion.identity, 0.1f, snap, Handles.CircleHandleCap);

        return (newManipulatorBefore, newManipulatorAfter);
    }
    private static void ChangeManipulatorsPositions(BezierPoint bezierPoint, Vector3 newManipulatorBefore, Vector3 newManipulatorAfter)
    {
        if (newManipulatorBefore != bezierPoint.manipulatorBefore)
        {
            bezierPoint.manipulatorBefore = newManipulatorBefore;
            if (bezierPoint.areManipulatorsSingle == false)
            {
                bezierPoint.manipulatorAfter = -bezierPoint.manipulatorBefore;
            }
        }
        else
        {
            bezierPoint.manipulatorAfter = newManipulatorAfter;
            if (bezierPoint.areManipulatorsSingle == false)
            {
                bezierPoint.manipulatorBefore = -bezierPoint.manipulatorAfter;
            }
        }

    }
    private static void ChangeSingleStatus(BezierPoint bezierPoint)
    {
        Event e = Event.current;

        if (Event.current.clickCount > 1)
        {
            bezierPoint.areManipulatorsSingle = false;
            Vector3 sum = (bezierPoint.manipulatorBefore + bezierPoint.manipulatorAfter);
            if (sum != Vector3.zero)
            {
                Vector3 cross = Vector3.Cross(bezierPoint.manipulatorBefore, bezierPoint.manipulatorAfter);
                float lenght = (bezierPoint.manipulatorBefore.magnitude + bezierPoint.manipulatorAfter.magnitude) / 2;
                bezierPoint.manipulatorBefore = (Vector3.Cross(sum, cross)).normalized * lenght;
                bezierPoint.manipulatorAfter = -bezierPoint.manipulatorBefore;
            }

        }
        else if (e.shift && e.button == 0 && e.type == EventType.Used
            && !(e.keyCode == KeyCode.W || e.keyCode == KeyCode.A 
            || e.keyCode == KeyCode.S || e.keyCode == KeyCode.D))
        {
            bezierPoint.areManipulatorsSingle = true;
        }
    }
}
