using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewCtrl : MonoBehaviour, IDragHandler
{
    public Button upcamera_left;
    public Button leftcamera_down;
    public Button rightcamera_up;
    public Button downcamera_right;

    public Button resetBtn;
    public Transform Axis;
    public Camera camera;
    public float speed;
    public float maxVerticalRotationAngle = 90f;

    private Quaternion oriAngle;
    private float oriScale;

    private float cumulativeRotationX = 0f;

    private float xRotation = 0f;
    private float yRotation = 0f;
    public float xRotationSpeed = 5f;
    public float yRotationSpeed = 5f;
    public float yRotationMinLimit = -80f;
    public float yRotationMaxLimit = 80f;

    private void Start()
    {
        oriAngle = Axis.rotation;
        oriScale = camera.fieldOfView;

        resetBtn.onClick.AddListener(Reset);

        upcamera_left.onClick.AddListener(() => { RotateCamera(-10f, 0f); });
        downcamera_right.onClick.AddListener(() => { RotateCamera(10f, 0f); });
        leftcamera_down.onClick.AddListener(() => { RotateCamera(0f, -10f); });
        rightcamera_up.onClick.AddListener(() => { RotateCamera(0f, 10f); });
    }

private void Reset()
{
    Axis.rotation = oriAngle;
    camera.fieldOfView = oriScale;
    cumulativeRotationX = 0f;
    
    // Reset X and Y rotation angles
    xRotation = 0f;
    yRotation = 0f;
}


    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;

        if (Input.GetMouseButton(0))
        {
            // Rotation logic based on mouse movement
            xRotation -= delta.x * xRotationSpeed * 0.1f;
            yRotation += delta.y * yRotationSpeed * 0.1f;
            yRotation = ClampValue(yRotation, yRotationMinLimit, yRotationMaxLimit);

            // Apply the rotation to Transform.rotation
            Quaternion rotation = Quaternion.Euler(-yRotation, -xRotation, 0);
            Axis.rotation = rotation;
        }
    }

    void RotateCamera(float deltaX, float deltaY)
    {
        // Rotation logic based on button click
        xRotation -= deltaX * xRotationSpeed * 0.1f;
        yRotation += deltaY * yRotationSpeed * 0.1f;
        yRotation = ClampValue(yRotation, yRotationMinLimit, yRotationMaxLimit);

        // Apply the rotation to Transform.rotation
        Quaternion rotation = Quaternion.Euler(-yRotation, -xRotation, 0);
        Axis.rotation = rotation;
    }

    float ClampValue(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    private void Update()
    {
        float wheelFloat = Input.GetAxis("Mouse ScrollWheel");
        camera.fieldOfView += wheelFloat * speed;
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 1, 80);
    }
}
