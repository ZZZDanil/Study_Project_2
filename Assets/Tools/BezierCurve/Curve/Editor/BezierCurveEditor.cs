using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveEditor : Editor
{
    private SerializedProperty bezierPointsSP;
    private BezierCurve bezierCurve;

    private void OnEnable()
    {
        bezierCurve = (BezierCurve)target;
        bezierPointsSP = serializedObject.FindProperty("bezierPoints");
    }

    public override void OnInspectorGUI()
    {
        if (bezierCurve.bezierPoints != null && bezierCurve.bezierPoints.Count > 0)
        {
            GUILayout.Label("Initiated");
        }
        else
        {
            if (GUILayout.Button("Init First point in center"))
            {
                BezierCurveSystem.InitFirstPoint(bezierCurve);
            }
        }

        EditorGUILayout.PropertyField(bezierPointsSP);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        BezierCurveDrawSystem.DrawBezier(bezierCurve);
    }

    [MenuItem("GameObject/Tool/BezierCurve")]
    static void ContextMenu(MenuCommand menuCommand)
    {
        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(
            AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/Tools/BezierCurve/Curve/BezierCurve.prefab")
            );
        if (prefab != null)
        {
            if (Selection.activeTransform != null)
            {
                prefab.transform.SetParent(Selection.activeTransform, false);
            }
            prefab.transform.localPosition = Vector3.zero;
            prefab.transform.localEulerAngles = Vector3.zero;
            prefab.transform.localScale = Vector3.one;

            PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            Selection.activeGameObject = prefab;
        }
    }
}
