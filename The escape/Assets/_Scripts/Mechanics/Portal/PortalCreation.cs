using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Mechanics.Portal
{
    public class PortalCreation : MonoBehaviour
    {
        private List<Portal> portalList = new List<Portal>();

        private PlayerInputActions inputActions;
        private InputAction touchInput;
        private InputAction touchPosition;

        private Vector2 startTouchPosition;
        private Vector2 endTouchPosition;

        private bool canCreatePortal = true;

        private Camera mainCamera;

        [SerializeField] private GameObject portal;
        [SerializeField] private int maxPortalCount;
        private int currentPortalIndex;

        private float portalLength;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            mainCamera = Camera.main;
            //Get extreme portal point and save info about portal's length
            portalLength = Vector3.Distance(portal.transform.position, portal.transform.GetChild(0).position);
        }

        private void OnEnable()
        {
            inputActions.GameProcess.Enable();

            touchInput = inputActions.GameProcess.TouchInput;
            touchPosition = inputActions.GameProcess.TouchPosition;

            touchInput.performed += SaveStartTouchPosition;
            touchInput.canceled += SaveEndTouchPosition;

            touchInput.Enable();
            touchPosition.Enable();
        }

        private void Start()
        {
            for (int i = 0; i < maxPortalCount; i++)
            {
                GameObject createdPortal = Instantiate(portal, transform.position, Quaternion.identity);
                portalList.Add(createdPortal.GetComponent<Portal>());
            }
            Debug.Log(portalList.Count);
            for (int i = 0; i < portalList.Count; i++)
            {
                Debug.Log(portalList[i].gameObject.name);
                if (i == portalList.Count - 1)
                    portalList[i].anotherPortal = portalList[0];
                else
                    portalList[i].anotherPortal = portalList[i + 1];

                portalList[i].gameObject.SetActive(false);
                portalList[i].gameObject.GetComponent<Portal>().IsActivePortal = false;
            }
        }

        private void SaveStartTouchPosition(InputAction.CallbackContext obj)
        {
            startTouchPosition = touchPosition.ReadValue<Vector2>();
        }

        private void SaveEndTouchPosition(InputAction.CallbackContext obj)
        {
            endTouchPosition = touchPosition.ReadValue<Vector2>();
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

        private void CreatePortal()
        {
            GameObject createdPortal = portalList[currentPortalIndex].gameObject;
            createdPortal.transform.position = startTouchPosition;
            createdPortal.GetComponent<Portal>().IsActivePortal = true;
            createdPortal.SetActive(true);
            if (currentPortalIndex == portalList.Count - 1)
            {
                currentPortalIndex = 0;
            }
            else
            {
                currentPortalIndex++;
            }
            Vector2 diff = endTouchPosition - (Vector2)createdPortal.transform.position;
            float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            createdPortal.transform.rotation = Quaternion.Euler(0, 0, z);

            // return createdPortal.GetComponent<Portal>();
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
            touchInput.Disable();
            touchPosition.Disable();
        }
    }
}