using UnityEngine;

public class AIMoving : MonoBehaviour
{

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed = 10;

    private bool _moveDirection = true;

    void FixedUpdate()
    {
        if (_moveDirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.position, _speed * Time.fixedDeltaTime);
            if (transform.position == _endPoint.position) { _moveDirection = false; }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPoint.position, _speed * Time.fixedDeltaTime);
            if (transform.position == _startPoint.position) { _moveDirection = true; }
        }

    }
}
