using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace _Scripts.Units.Player.Movement
{
    public class FloatingJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Header("Joystick Parameters")]
        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone;
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform handle;
        
        [Space]
        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;
        
        [Header("Joystick Anim Parameters")]
        [SerializeField] private float stepForAlpha = 0.01f;
        [SerializeField] private float timeUpdateAlpha = 0.01f;

        private RectTransform baseRect;
        private Image imageOfBackground;
        private Image imageOfHandle;
        private Canvas canvas;
        private Camera cam;
    
        private Vector2 input = Vector2.zero;
    
        protected virtual void Start()
        {
            HandleRange = handleRange;
            DeadZone = deadZone;
            baseRect = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            imageOfBackground = background.GetComponent<Image>();
            imageOfHandle = handle.GetComponent<Image>();
            if (canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
        
            background.gameObject.SetActive(false);
        }
        
        public float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }

        public float DeadZone
        {
            get => deadZone;
            set => deadZone = Mathf.Abs(value);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));
            
            StopAllCoroutines();
            
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            JoystickTurningOn();
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));
        
            cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            Vector2 radius = background.sizeDelta / 2;
            input = (eventData.position - position) / (radius * canvas.scaleFactor);
        
            SendValueToControl(input);
        
            HandleInput(input.magnitude, input.normalized);
            handle.anchoredPosition = input * radius * handleRange;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartCoroutine(JoystickTurningOff());
        
            SendValueToControl(Vector2.zero);
            handle.anchoredPosition = Vector2.zero;
        }

        private void HandleInput(float magnitude, Vector2 normalised)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    input = normalised;
            }
            else
                input = Vector2.zero;
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out var localPoint))
            {
                Vector2 sizeDelta;
                Vector2 pivotOffset = baseRect.pivot * (sizeDelta = baseRect.sizeDelta);
                return localPoint - (background.anchorMax * sizeDelta) + pivotOffset;
            }
            return Vector2.zero;
        }

        IEnumerator JoystickTurningOff()
        {
            float alpha = 1f;

            while (imageOfBackground.color.a >= 0f)
            {
                alpha -= stepForAlpha;
                imageOfHandle.color = imageOfBackground.color = new Color(1f, 1f, 1f, alpha);

                yield return new WaitForSeconds(timeUpdateAlpha);

                if (imageOfBackground.color.a <= 0)
                    background.gameObject.SetActive(false);
            }
        }

        private void JoystickTurningOn()
        {
            background.gameObject.SetActive(true);
            imageOfHandle.color = imageOfBackground.color = new Color(1f, 1f, 1f, 1f);
        }

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}
