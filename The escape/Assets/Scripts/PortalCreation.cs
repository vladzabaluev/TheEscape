using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalCreation : MonoBehaviour
{
    [SerializeField] private GameObject Portal;
    private PlayerInput inputActions;
    private InputAction TouchInput;
    private InputAction TouchPosition;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private bool canCreatePortal = true;

    private Camera mainCamera;

    private Transform portalEndPosition;

    private float portalLength;

    private void Awake()
    {
        inputActions = new PlayerInput();
        mainCamera = Camera.main;

        portalEndPosition = Portal.transform.GetChild(0);
        portalLength = Vector3.Distance(Portal.gameObject.transform.position, portalEndPosition.position);
    }

    private void OnEnable()
    {
        inputActions.GameProcess.Enable();

        TouchInput = inputActions.GameProcess.TouchInput;
        TouchPosition = inputActions.GameProcess.TouchPosition;

        TouchInput.performed += SaveStartTouchPosition;
        TouchInput.canceled += SaveEndTouchPosition;

        TouchInput.Enable();
        TouchPosition.Enable();
    }

    private void SaveStartTouchPosition(InputAction.CallbackContext obj)
    {
        Debug.Log(TouchPosition.ReadValue<Vector2>());
        startTouchPosition = TouchPosition.ReadValue<Vector2>();
    }

    private void SaveEndTouchPosition(InputAction.CallbackContext obj)
    {
        endTouchPosition = TouchPosition.ReadValue<Vector2>();
        CheckSpawnZone();
    }

    private void CheckSpawnZone()
    {
        Ray castRay = mainCamera.ScreenPointToRay(startTouchPosition);

        if (!Physics2D.Raycast(mainCamera.ScreenToWorldPoint(startTouchPosition),
          mainCamera.ScreenToWorldPoint(endTouchPosition) - mainCamera.ScreenToWorldPoint(startTouchPosition), portalLength))
        {
            if (canCreatePortal)
            {
                startTouchPosition = mainCamera.ScreenToWorldPoint(startTouchPosition);
                endTouchPosition = mainCamera.ScreenToWorldPoint(endTouchPosition);
                CreatePortal();
            }
        }
    }

    private void CreatePortal()
    {
        GameObject createdPortal = Instantiate(Portal, startTouchPosition, Quaternion.identity);
        Vector2 diff = endTouchPosition - (Vector2)createdPortal.transform.position;
        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        createdPortal.transform.rotation = Quaternion.Euler(0, 0, z);
    }

    private void SetTheAbilityOfPortalCreation(bool canCreatePortal)
    {
        this.canCreatePortal = canCreatePortal;
    }

    private void OnDisable()
    {
        TouchInput.Disable();
        TouchPosition.Disable();
    }
}