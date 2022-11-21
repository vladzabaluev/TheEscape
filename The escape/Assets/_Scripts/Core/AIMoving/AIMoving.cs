using System;
using UnityEngine;

public class AIMoving : MonoBehaviour
{
	[SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
	[SerializeField] private Transform _transform;
    [SerializeField] private float _speed = 10;

	private bool _moveDirection = true;

	private void FixedUpdate()
    {
        if (_moveDirection)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _endPoint.position, _speed * Time.fixedDeltaTime);
			if (_transform.position == _endPoint.position)
			{
				_moveDirection = false;
			}
        }
        else
        {
			_transform.position = Vector3.MoveTowards(_transform.position, _startPoint.position, _speed * Time.fixedDeltaTime);
			if (_transform.position == _startPoint.position)
			{
				_moveDirection = true;
			}
        }
	}
}
