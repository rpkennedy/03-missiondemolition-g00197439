using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    [Header("Set in Unity")]
    public GameObject prefabProjectile;
    public float velocityMult;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimMode;
    private Rigidbody rbProjectile;


    void Awake()
    {
        Transform launchTrans = transform.Find("LaunchPoint");
        launchPoint = launchTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchTrans.position;
    }

    void Update()
    {
        if (!aimMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimMode = false;
            rbProjectile.isKinematic = false;
            rbProjectile.velocity = -mouseDelta * velocityMult;
            CameraController.POI = projectile;
            projectile = null;
        }
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimMode = true;

        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;

        rbProjectile = projectile.GetComponent<Rigidbody>();
        rbProjectile.isKinematic = true;
    }
}
