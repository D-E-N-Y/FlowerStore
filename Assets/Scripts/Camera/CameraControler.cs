using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Camera _camera;
    
    [SerializeField, Range(0.1f, 5f)] private float movementTime;
    [SerializeField, Range(0.1f, 5f)] private float zoomAmount;
    
    [SerializeField, Range(10, 20)] private float maxZoom;
    [SerializeField, Range(1, 10)] private float minZoom;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    private void Start() 
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;

        _camera = cameraTransform.gameObject.GetComponent<Camera>();
    }

    private void Update() 
    {
        HandleMouseInput();

        Control();
    }


    private void Control()
    {
        // move
        transform.position = Vector3.Lerp(transform. position, newPosition, Time.deltaTime * movementTime);

        // rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);

        // zoom
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    private bool isDrag = false;
    private void HandleMouseInput()
    {
        // if (EventSystem.current.IsPointerOverGameObject()) 
        // {
        //     isDrag = false;
        //     return;
        // }
        
        // move
        if(Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
                dragStartPosition = ray.GetPoint(entry);

            isDrag = true;
        }

        if(Input.GetMouseButton((int)MouseButton.Right) && isDrag)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            { 
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        if(Input.GetMouseButtonUp((int)MouseButton.Right))
        {
            isDrag = false;
        }


        // rotation
        if(Input.GetMouseButtonDown((int)MouseButton.Middle))
            rotateStartPosition = Input.mousePosition;
        
        if(Input.GetMouseButton((int)MouseButton.Middle))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }


        // zoom
        if(Input.mouseScrollDelta.y != 0)
            newZoom += Input.mouseScrollDelta.y * (cameraTransform.localPosition * -zoomAmount);

        if(newZoom.z < -maxZoom)
            newZoom = new Vector3(newZoom.x, maxZoom, -maxZoom);

        if(newZoom.z > -minZoom)
            newZoom = new Vector3(newZoom.x, minZoom, -minZoom);
    }
}
