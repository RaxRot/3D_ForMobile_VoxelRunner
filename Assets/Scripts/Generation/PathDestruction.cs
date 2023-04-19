using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDestruction : MonoBehaviour
{
    [HideInInspector]public static float zPosition;

    private void Start()
    {
        zPosition = transform.position.z;
    }
}
