using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public GameObject launchPoint;


    void Awake()
    {
        Transform launchTrans = transform.Find("LaunchPoint");
        launchPoint = launchTrans.gameObject;
        launchPoint.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }
}
