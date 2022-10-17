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

    private float portalLength;

    private void Awake()
    {
        inputActions = new PlayerInput();
        mainCamera = Camera.main;
        //Get extreme portal point and save info about portal's length
        portalLength = Vector3.Distance(Portal.transform.position, Portal.transform.GetChild(0).position);
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
        startTouchPosition = mainCamera.ScreenToWorldPoint(startTouchPosition);
        endTouchPosition = mainCamera.ScreenToWorldPoint(endTouchPosition);

        if (!Physics2D.Raycast((startTouchPosition),
          (endTouchPosition) - (startTouchPosition), portalLength))
        {
            if (canCreatePortal)
            {
                CreatePortal();
            }
        }
    }

    private Portal CreatePortal()
    {
        var createdPortal = Instantiate(Portal, startTouchPosition, Quaternion.identity);
        Vector2 diff = endTouchPosition - (Vector2)createdPortal.transform.position;
        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        createdPortal.gameObject.transform.rotation = Quaternion.Euler(0, 0, z);

        return createdPortal.GetComponent<Portal>();
    }

    //private void OnTakePortalFromPool(Portal portal)
    //{
    //    portal.gameObject.SetActive(true);
    //}

    //private void OnReturnPortalToPool(Portal portal)
    //{
    //    portal.gameObject.SetActive(true);
    //}

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