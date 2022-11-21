using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalCreation : MonoBehaviour, IPauseHandler
{
	[SerializeField] private GameObject _portal;
	[SerializeField] private int _maxPortalCount;

	private readonly List<Portal> _portalList = new();

	private PlayerInputActions _inputActions;
	private InputAction _touchInput;
	private InputAction _touchPosition;

	private Vector2 _startTouchPosition;
	private Vector2 _endTouchPosition;
	private Camera _mainCamera;

	private int _currentPortalIndex;
	private float _portalLength;
	private bool _canCreatePortal = true;

	private PauseManager PauseManager => ProjectContext.Instance.PauseManager;

	private void Awake()
	{
		_inputActions = new PlayerInputActions();
		_mainCamera = Camera.main;
		PauseManager.Register(this);

		// Get extreme portal point and save info about portal's length.
		_portalLength = Vector3.Distance(_portal.transform.position, _portal.transform.GetChild(0).position);
	}

	private void OnEnable()
	{
		_inputActions.GameProcess.Enable();

		_touchInput = _inputActions.GameProcess.TouchInput;
		_touchPosition = _inputActions.GameProcess.TouchPosition;

		_touchInput.performed += SaveStartTouchPosition;
		_touchInput.canceled += SaveEndTouchPosition;

		_touchInput.Enable();
		_touchPosition.Enable();
	}

	private void Start()
	{
		for (int i = 0; i < _maxPortalCount; i++)
		{
			GameObject createdPortal = Instantiate(_portal, transform.position, Quaternion.identity);
			_portalList.Add(createdPortal.GetComponent<Portal>());
		}

		for (int i = 0; i < _portalList.Count; i++)
		{
			if (i == _portalList.Count - 1)
			{
				_portalList[i].AnotherPortal = _portalList[0];
			}
			else
			{
				_portalList[i].AnotherPortal = _portalList[i + 1];
			}

			_portalList[i].gameObject.SetActive(false);
			_portalList[i].gameObject.GetComponent<Portal>().IsActivePortal = false;
		}
	}

	private void SaveStartTouchPosition(InputAction.CallbackContext obj)
	{
		_startTouchPosition = _touchPosition.ReadValue<Vector2>();
	}

	private void SaveEndTouchPosition(InputAction.CallbackContext obj)
	{
		_endTouchPosition = _touchPosition.ReadValue<Vector2>();
		CheckSpawnZone();
	}

	private void CheckSpawnZone()
	{
		_mainCamera.ScreenPointToRay(_startTouchPosition);

		_startTouchPosition = _mainCamera.ScreenToWorldPoint(_startTouchPosition);
		_endTouchPosition = _mainCamera.ScreenToWorldPoint(_endTouchPosition);

		if (!Physics2D.Raycast((_startTouchPosition), _endTouchPosition - _startTouchPosition, _portalLength))
			if (_canCreatePortal)
				CreatePortal();
	}

	private void CreatePortal()
	{
		GameObject createdPortal = _portalList[_currentPortalIndex].gameObject;
		createdPortal.transform.position = _startTouchPosition;
		createdPortal.GetComponent<Portal>().IsActivePortal = true;
		createdPortal.SetActive(true);

		if (_currentPortalIndex == _portalList.Count - 1)
			_currentPortalIndex = 0;
		else
			_currentPortalIndex++;

		Vector2 diff = _endTouchPosition - (Vector2)createdPortal.transform.position;

		float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		createdPortal.transform.rotation = Quaternion.Euler(0, 0, z);
	}

	private void SetTheAbilityOfPortalCreation(bool canCreatePortal)
	{
		_canCreatePortal = canCreatePortal;
	}

	private void OnDisable()
	{
		_touchInput.Disable();
		_touchPosition.Disable();
		PauseManager.UnRegister(this);
	}

	void IPauseHandler.SetPaused(bool isPaused)
	{
		_canCreatePortal = !isPaused;
	}
}
