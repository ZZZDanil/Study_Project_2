using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[CustomEditor(typeof(BezierPoint))]
public class BezierPointEditor : Editor
{
    private BezierPoint bezierPoint;
    private BezierCurve bezierCurve;

    private SerializedProperty manipulatorBeforeSP;
    private SerializedProperty manipulatorAfterSP;
    private SerializedProperty isCanDestroyedSP;
    private SerializedProperty areManipulatorsSingleSP;


    private Color basicBackgroundColor;
#if BEZIER_POINT_EDITOR_MORE_DETAILS
    private Color basicContentColor;
    private Color basicColor;
#endif

    private void Awake()
    {
        bezierPoint = (BezierPoint)target;
        if (bezierPoint != null) {
            bezierCurve = bezierPoint.transform.GetComponentInParent<BezierCurve>(); ;
        }

        manipulatorBeforeSP = serializedObject.FindProperty("manipulatorBefore");
        manipulatorAfterSP = serializedObject.FindProperty("manipulatorAfter");
        isCanDestroyedSP = serializedObject.FindProperty("isCanDestroyed");
        areManipulatorsSingleSP = serializedObject.FindProperty("areManipulatorsSingle");

        basicBackgroundColor = GUI.backgroundColor;

#if BEZIER_POINT_EDITOR_MORE_DETAILS
        basicContentColor = GUI.contentColor;
        basicColor = GUI.color;
#endif

        if (Application.isEditor && !Application.isPlaying)
        {
            if (bezierPoint.isCanDestroyed == false)
            {
                BezierPointSystem.FindBezierPointByNameAndDestroyExcess(bezierPoint, bezierCurve);
            }
        }
    }

    private void OnSceneGUI()
    {
        BezierPointDrawSystem.DrawGUI(bezierPoint);
    }

    public override void OnInspectorGUI()
    {
        if (!BezierPointSystem.IsInPefabMode(bezierPoint) && bezierCurve != null)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            int pointPosition = BezierPointSystem.PointsPositionInBezier(bezierPoint, bezierCurve);

            if (pointPosition != 0)
                GUI.backgroundColor = Color.green;
            else
                GUI.backgroundColor = Color.grey;

            if (GUILayout.Button("Add"))
            {
                if (bezierCurve != null)
                {
                    if (pointPosition == 1)
                        BezierCurveLoadPointSystem.AddPoint(bezierCurve, bezierPoint.transform.position + Vector3.right);
                    else if (pointPosition == -1)
                        BezierCurveLoadPointSystem.AddPointToFirst(bezierCurve, bezierPoint.transform.position + Vector3.right);
                }
            }

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Del"))
            {
                bezierPoint.isCanDestroyed = true;
            }

            GUILayout.EndHorizontal();

#if BEZIER_POINT_EDITOR_MORE_DETAILS
            GUI.backgroundColor = basicBackgroundColor;

            EditorGUILayout.BeginHorizontal();
            GUI.color = Color.red;
            EditorGUILayout.PrefixLabel("Before");
            GUI.color = basicColor;
            EditorGUILayout.Vector3Field("", bezierPoint.manipulatorBefore);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUI.color = Color.green;
            EditorGUILayout.PrefixLabel("After");
            GUI.color = basicColor;
            EditorGUILayout.Vector3Field("", bezierPoint.manipulatorAfter);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(isCanDestroyedSP);
            EditorGUILayout.PropertyField(areManipulatorsSingleSP);
#endif

            serializedObject.ApplyModifiedProperties();

            if (bezierPoint.isCanDestroyed == true)
            {
                if (bezierCurve != null)
                {
                    int delIndex = BezierCurveSystem.FindBezierPointIndex(bezierCurve, bezierPoint);
                    if (delIndex > -1)
                        bezierCurve.bezierPoints.RemoveAt(delIndex);
                }
                DestroyImmediate(bezierPoint.gameObject);
            }
        }
    }
}
