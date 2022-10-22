using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AIMoving : MonoBehaviour
{

    [SerializeField] private Transform StartPoint;
    [SerializeField] private Transform EndPoint;
    private bool moveDirecton = true;
    void Start()
    {
        StartPoint.position = transform.position;
        EndPoint.position = transform.position + new Vector3(5.0f,0,0);
    }

    void FixedUpdate()
    {
        if (moveDirecton)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, 0.13f);
            if (transform.position == EndPoint.position) { moveDirecton = false; }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, 0.13f);
            if (transform.position == StartPoint.position) { moveDirecton = true; }
        }

    }
}
