using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static public GameObject POI;
    private Vector2 minXY = Vector2.zero;

    [Header("Set in Unity")]
    public float easing;


    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        if (POI == null) return;

        Vector3 dest = POI.transform.position;

        dest.x = Mathf.Max(minXY.x, dest.x);
        dest.y = Mathf.Max(minXY.y, dest.y);

        dest = Vector3.Lerp(transform.position, dest, easing);
        dest.z = camZ;
        transform.position = dest;

        Camera.main.orthographicSize = dest.y + 10;
    }
}
