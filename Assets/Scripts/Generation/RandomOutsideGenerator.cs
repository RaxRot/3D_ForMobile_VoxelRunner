using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomOutsideGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] outSideObjects;
    private int _outsideIndex;

    [SerializeField] private Transform[] spawnPosition;
    private int _spawnPositionIndex;

    [SerializeField] private float minTimeToSpawn = 2f;
    [SerializeField] private float maxTimeToSpawn = 5f;
    private float _timeToSpawn;
    
    private float _firstWorldSpeed;

    private void Start()
    {
        _firstWorldSpeed = GameManager.Instance.GetWorldSpeed();
        
        SpawnOutsideObj();
    }

    private void SpawnOutsideObj()
    {
        StartCoroutine(nameof(_SpawnOutsideObjCo));
    }

    private IEnumerator _SpawnOutsideObjCo()
    {
        _outsideIndex = Random.Range(0, outSideObjects.Length);
        _spawnPositionIndex = Random.Range(0, spawnPosition.Length);

        Instantiate(outSideObjects[_outsideIndex], spawnPosition[_spawnPositionIndex].position, transform.rotation);

        _timeToSpawn = Random.Range(minTimeToSpawn*_firstWorldSpeed/GameManager.Instance.GetWorldSpeed(), 
            maxTimeToSpawn*_firstWorldSpeed/GameManager.Instance.GetWorldSpeed());

        yield return new WaitForSeconds(_timeToSpawn);

        if (GameManager.Instance.canMove)
        {
            SpawnOutsideObj();
        }
    }
    
    
}
