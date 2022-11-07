using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AIMoving : MonoBehaviour
{

    [SerializeField] private Transform StartPoint;
    [SerializeField] private Transform EndPoint;
    [SerializeField] private float speed = 10;

    private bool moveDirecton = true;

    void FixedUpdate()
    {
        if (moveDirecton)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, speed*Time.fixedDeltaTime);
            if (transform.position == EndPoint.position) { moveDirecton = false; }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, speed * Time.fixedDeltaTime);
            if (transform.position == StartPoint.position) { moveDirecton = true; }
        }

    }
}
