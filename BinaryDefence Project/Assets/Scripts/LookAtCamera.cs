using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera camToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        camToLookAt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = camToLookAt.transform.position - transform.position;

        v.x = v.z = 0.0f;
        transform.LookAt(camToLookAt.transform.position - v);
        transform.rotation = (camToLookAt.transform.rotation);
    }
}
