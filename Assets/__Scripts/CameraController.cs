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
        Vector3 dest;

        if (POI == null)
        {
            dest = Vector3.zero;
        }
        else
        {
            dest = POI.transform.position;

            if(POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }

        dest.x = Mathf.Max(minXY.x, dest.x);

        dest.x = Mathf.Max(minXY.x, dest.x);
        dest.y = Mathf.Max(minXY.y, dest.y);

        dest = Vector3.Lerp(transform.position, dest, easing);
        dest.z = camZ;
        transform.position = dest;

        Camera.main.orthographicSize = dest.y + 10;
    }
}
