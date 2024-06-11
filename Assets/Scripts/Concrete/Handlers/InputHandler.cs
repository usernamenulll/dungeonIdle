namespace Dungeon.Handlers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class InputHandler : MonoBehaviour
    {
        enum DeviceType
        {
            Mobile,
            PC
        }
        CameraHandler cameraControl;
        [SerializeField] DeviceType deviceType = DeviceType.Mobile;
        [SerializeField] LayerMask _layerMask;

        Vector3 touchStart;

        void Start()
        {
            cameraControl = GameObject.FindObjectOfType<CameraHandler>();
        }
        void Update()
        {
            switch (deviceType)
            {
                case DeviceType.PC:
                    PcPanZoom();
                    break;
                case DeviceType.Mobile:
                    MobilePanZoom();
                    break;
                default:
                    PcPanZoom();
                    break;
            }
        }

        void MobilePanZoom()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    touchStart = Camera.main.ScreenToWorldPoint(touch.position);
                    HitCheck(touch);
                }
                else if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                    float difference = currentMagnitude - prevMagnitude;

                    cameraControl.Zoom(difference);
                }
                else if ((touch.phase == TouchPhase.Moved))
                {
                    Vector3 dir = touchStart - Camera.main.ScreenToWorldPoint(touch.position);
                    cameraControl.DragCamera(dir);
                }
            }
        }
        void PcPanZoom()
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 dir = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                cameraControl.DragCamera(dir);
            }
            Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
            cameraControl.Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        void HitCheck(Touch touch)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            Physics.Raycast(ray.origin, ray.direction, out hit, _layerMask);
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Touchable"))
                {
                    cameraControl.MoveCamera(hit.transform);
                    GameManager.Instance.HitHandler(hit.transform.gameObject);
                }
            }
        }

    }
}

