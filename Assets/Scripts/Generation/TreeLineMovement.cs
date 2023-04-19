using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLineMovement : MonoBehaviour
{
    [SerializeField] private Transform disappearPoint;
    [SerializeField] private float newSpawnPoint = 28f;
    
    private void Update()
    {
        ControlObjectMovement();
    }

    private void ControlObjectMovement()
    {
        if (GameManager.Instance.canMove)
        {
            transform.position -= new Vector3(0f, 0f, GameManager.Instance.GetWorldSpeed() * Time.deltaTime);
        }

        if (transform.position.z<disappearPoint.position.z)
        {
            transform.position= new Vector3(transform.position.x, 0f, newSpawnPoint);
        }
    }
}
