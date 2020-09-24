using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    static public LineController s;

    [Header("Set in Unity")]
    public float min;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    void Awake()
    {
        s = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }

    void Update()
    {
        
    }

    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;

            if(_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;

        if (points.Count > 0 && (pt - lastPoint).magnitude < min) return;

        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - SlingshotController.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }       
    }

    public Vector3 lastPoint
    {
        get
        {

            if (points == null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }

    void FixedUpdate()
    {

        if (poi == null)
        {
            if (CameraController.POI != null)
            {
                if (CameraController.POI.tag == "Projectile")
                {
                    poi = CameraController.POI;
                }
                else
                {
                    return;

                }

            }
            else
            {
                return;
            }
        }
        AddPoint();

        if (CameraController.POI == null)
        {
            poi = null;
        }
    }
}
