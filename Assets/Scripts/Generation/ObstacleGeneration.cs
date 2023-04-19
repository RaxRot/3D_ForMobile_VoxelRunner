using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGeneration : MonoBehaviour
{
   
   [Header("Coins")]
   [SerializeField] private GameObject[] coins;
   private int _coinSpawnIndex;
   [SerializeField] private float[] heightCoinSpawn;
   private float _heightToSpawn;
   private Vector3 _spawnPosition;

   [Header("Obstacles")]
   [SerializeField] private GameObject [] obstacles;
   private int _obstacleIndex;
   [SerializeField] private float timeBetweenObstacleSpawn;
   private float _timerSpawnCounter;
   [SerializeField] private float rotationAngle = 45f;
   
   [SerializeField] private float minimizeSpawnTime = 0.75f;
   [SerializeField] private float maximizeSpawnTime = 1.25f;

   private float _firstWorldSpeed;
   
   
   private int _indexToSpawn;

   private void Start()
   {
      _timerSpawnCounter = timeBetweenObstacleSpawn;

      _firstWorldSpeed = GameManager.Instance.GetWorldSpeed();
   }

   private void Update()
   {
      ControlSpawn();
   }

   private void ControlSpawn()
   {
      if (GameManager.Instance.canMove)
      {
         _timerSpawnCounter -= Time.deltaTime;
         if (_timerSpawnCounter<=0)
         {
            if (Random.Range(0,100)<=40)
            {
               SpawnCoin();
            }
            else
            {
               SpawnObstacle();
            }
            
            _timerSpawnCounter = Random.Range(timeBetweenObstacleSpawn*minimizeSpawnTime*_firstWorldSpeed/GameManager.Instance.GetWorldSpeed(),
               timeBetweenObstacleSpawn*maximizeSpawnTime*_firstWorldSpeed/GameManager.Instance.GetWorldSpeed());
         }
      }
   }

   private void SpawnObstacle()
   {
      _obstacleIndex = Random.Range(0, obstacles.Length);
      
      Instantiate(obstacles[_obstacleIndex], transform.position,
         Quaternion.Euler(0f,Random.Range(-rotationAngle,rotationAngle),0f));
   }

   private void SpawnCoin()
   {
      _coinSpawnIndex = Random.Range(0, coins.Length);
      _heightToSpawn = Random.Range(0, heightCoinSpawn.Length);
      _spawnPosition = new Vector3(transform.position.x, _heightToSpawn, transform.position.z);

      Instantiate(coins[_coinSpawnIndex], _spawnPosition, transform.rotation);
   }
}
