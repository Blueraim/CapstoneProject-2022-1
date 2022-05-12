using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image Joystick;
    private Image Footprint;
    private Vector2 touchPosition;

    private void Awake()
    {
        Joystick = GetComponent<Image>();
        Footprint = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Touch Begin : " + eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Joystick.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = (touchPosition.x / Joystick.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / Joystick.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2, touchPosition.y * 2);

            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            Footprint.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * Joystick.rectTransform.sizeDelta.x / 2,
                touchPosition.y * Joystick.rectTransform.sizeDelta.y / 2);
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Footprint.rectTransform.anchoredPosition = Vector2.zero;

        touchPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }
    public float Vertical()
    {
        return touchPosition.y;
    }


}