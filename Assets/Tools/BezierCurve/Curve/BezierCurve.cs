using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    [SerializeField, HideInInspector] public List<BezierPoint> bezierPoints;


    void OnDrawGizmos()
    {
        if (!Selection.Contains(gameObject))
        {
            BezierCurveDrawSystem.DrawBezier(this);
        }
    }
    private void OnDestroy()
    {
        BezierCurveSystem.DeleteCurve(this);
    }
}
