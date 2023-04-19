using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathGeneration : MonoBehaviour
{
    [SerializeField] private GameObject [] pathPieces;
    private int _currentIndexToSpawn;
    
    [SerializeField] private Transform tresholdPoint;

    [SerializeField] private float pointToMove = 3.2f;

    [SerializeField] private Transform parent;

    private void Update()
    {
       ControlGeneration();
    }

    private void ControlGeneration()
    {
        if (transform.position.z<tresholdPoint.position.z)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        _currentIndexToSpawn = Random.Range(0, pathPieces.Length);
        Instantiate(pathPieces[_currentIndexToSpawn], transform.position, transform.rotation,parent);
        transform.position += new Vector3(0, 0, pointToMove);
    }
}
