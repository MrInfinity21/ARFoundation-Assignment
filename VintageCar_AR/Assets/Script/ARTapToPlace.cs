using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlace : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;

    [SerializeField] private GameObject ARPrefab;

    private GameObject _placedObject; //This is a referece to the instantiated object
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    Vector2 _touchPosition;

    private void TapToPlace(Vector2 point)
    {
        if (raycastManager.Raycast(point, _hits, TrackableType.PlaneWithinPolygon))
        {
            // if the object is placed already, just move. If not create one
            if (_placedObject)
            {
                _placedObject.transform.position = _hits[0].pose.position;
            }
            else
            {
                _placedObject = Instantiate(ARPrefab, _hits[0].pose.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        if (Touchscreen.current != null)
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                TapToPlace(Touchscreen.current.primaryTouch.position.ReadValue());
            }


        if (Mouse.current != null)
            if (Mouse.current.leftButton.isPressed)
            {
                TapToPlace(Mouse.current.position.ReadValue());
            }
    }
}
