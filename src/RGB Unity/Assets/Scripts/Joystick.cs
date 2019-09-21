using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{

    private Image jsContainer;
    private Image joystick;

    private Vector3 inputDirection;

    private void Start()
    {

        jsContainer = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>(); //this command is used because there is only one child in hierarchy
        inputDirection = Vector3.zero;
    }

    public void OnDrag(PointerEventData ped)
    {
        //To get InputDirection
        RectTransformUtility.ScreenPointToLocalPointInRectangle
                (jsContainer.rectTransform,
                ped.position,
                ped.pressEventCamera,
                out var position);

        var rectTransform = jsContainer.rectTransform;
        var sizeDelta = rectTransform.sizeDelta;
        position.x = (position.x / sizeDelta.x);
        position.y = (position.y / sizeDelta.y);

        var pivot = rectTransform.pivot;
        var x = pivot.x == 1f ? position.x * 2 + 1 : position.x * 2 - 1;
        var y = pivot.y == 1f ? position.y * 2 + 1 : position.y * 2 - 1;

        inputDirection = new Vector3(x, y, 0);
        inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

        //to define the area in which joystick can move around
        var delta = rectTransform.sizeDelta;
        joystick.rectTransform.anchoredPosition = new Vector3(inputDirection.x * (delta.x / 3)
                                                               , inputDirection.y * (delta.y) / 3);

    }

    public void OnPointerDown(PointerEventData ped)
    {

        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {

        inputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }

    public Vector2 GetDirection()
    {
        return new Vector2(inputDirection.x,inputDirection.y);
    }
}
