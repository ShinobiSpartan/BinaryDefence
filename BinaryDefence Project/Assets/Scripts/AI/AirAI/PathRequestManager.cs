using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {

    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector pathEnd;
        public Action<Vector3[], bool> callback;
    }
}
