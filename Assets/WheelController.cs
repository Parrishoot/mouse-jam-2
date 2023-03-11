using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public float rotationBuffer = 80;
    public CarMovementController carController;
    private Vector2 pivot;
    private Vector2 lastPos;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 lastAngle = pivot - lastPos;
        Vector2 currentAngle = pivot - eventData.position;

        float rotationChange = Vector2.SignedAngle(lastAngle, currentAngle);
        float currentRotation = transform.eulerAngles.z;

        float rotation = transform.eulerAngles.z;

        // if((currentRotation >= 0 && currentRotation <= rotationBuffer && rotationChange > 0) ||
        //    (currentRotation <= 360 && currentRotation >= 360 - rotationBuffer && rotationChange < 0)) {
            rotation += rotationChange;
        // }

        transform.eulerAngles = new Vector3(0, 0, rotation);
        carController.SetRotation(-transform.eulerAngles.z);
        lastPos = eventData.position;
    }

    public void Start() {
        pivot = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = eventData.position;
    }
}
