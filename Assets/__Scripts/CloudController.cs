using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("Set in Unity")]
    public int numClouds = 40;      
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f;
    private GameObject[] cloudInstances;

    private Vector3 scaleOffset = new Vector3(5, 2, 1);
    private Vector2 scaleRangex = new Vector2(4, 8);
    private Vector2 scaleRangey = new Vector2(3, 4);
    private Vector2 scaleRangez = new Vector2(2, 4);
    private float scaleMiny = 2f;
    private List<GameObject> spheres;


    void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;

        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU;
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;
        }
    }

    void Start()
    {
        spheres = new List<GameObject>();
        float num = Random.Range(cloudScaleMin, cloudScaleMax);

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudPrefab);
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            Vector3 offset = Random.insideUnitSphere;
            offset.x *= scaleOffset.x;
            offset.y *= scaleOffset.y;
            offset.z *= scaleOffset.z;
            spTrans.localPosition = offset;

            Vector3 scale = Vector3.one;
            scale.x = Random.Range(scaleRangex.x, scaleRangex.y);
            scale.y = Random.Range(scaleRangey.x, scaleRangey.y);
            scale.z = Random.Range(scaleRangez.x, scaleRangez.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / scaleOffset.x);
            scale.y = Mathf.Max(scale.y, scaleMiny);
            spTrans.localScale = scale;
        }
    }

    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            
            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;
        }
    }

    void Restart()
    {
        foreach (GameObject sp in spheres)
        {
            Destroy(sp);
        }

        Start();
    }
}
