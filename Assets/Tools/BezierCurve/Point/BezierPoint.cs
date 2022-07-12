using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
[ExecuteInEditMode]
public class BezierPoint : MonoBehaviour
{

    [SerializeField] public Vector3 point;

    [SerializeField] public Vector3 manipulatorBefore = Vector3.left;

    [SerializeField] public Vector3 manipulatorAfter = Vector3.right;

    [SerializeField, HideInInspector] public BezierCurve bezierCurve;

    [SerializeField, HideInInspector] public bool areManipulatorsSingle = false;
    [SerializeField, HideInInspector] public bool isCanDestroyed = true;


    void OnDrawGizmos()
    {
        if (!BezierPointSystem.IsInPefabMode(gameObject))
        {
            if (!Selection.Contains(gameObject))
            {
                BezierPointDrawSystem.DrawGUI(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (Application.isEditor && !Application.isPlaying)
        {

            if (isCanDestroyed == false)
            {
                GameObject go = Instantiate(gameObject, transform.position, transform.rotation, transform.parent.transform);
                if (go != null)
                {
                    go.name = name;
                    go.SetActive(true);
                }
                BezierPoint newBezierPoint = go.GetComponent<BezierPoint>();
                if (newBezierPoint != null)
                {
                    BezierCurve parent = transform.parent.GetComponent<BezierCurve>();
                    if (parent != null)
                    {
                        BezierCurveSystem.ChangeLinkOnObject(parent, newBezierPoint, GetInstanceID());
                    }
                }

            }
        }
    }
}
