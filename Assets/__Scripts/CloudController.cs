using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [Header("Set in Unity")]
    public GameObject cloudSphere;
    public int cloudMin;
    public int cloudMax;


    private Vector3 scaleOffset = new Vector3(5, 2, 1);
    private Vector2 scaleRangex = new Vector2(4, 8);
    private Vector2 scaleRangey = new Vector2(3, 4);
    private Vector2 scaleRangez = new Vector2(2, 4);
    private float scaleMiny = 2f;
    private List<GameObject> spheres;

    void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(cloudMin, cloudMax);

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
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
