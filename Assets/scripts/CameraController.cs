using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 2.0f;       // Speed of dragging movement
    public Slider zoomSlider;            // Reference to the UI Slider for zoom control
    public float minZoom = 100.0f;       // Minimum zoom value
    public float maxZoom = 1300.0f;      // Maximum zoom value

    private Vector3 dragOrigin;          // Original position when dragging started

    // Fixed boundary values (don't change with zoom)
    public float boundaryOffset = 500f;

    void Start()
    {
        // Ensure the camera is orthographic
        Camera.main.orthographic = true;

        // Set the initial position and zoom level of the camera (zoomed out, at maxZoom)
        Camera.main.transform.position = new Vector3(-100f, -400f, -950f);
        Camera.main.orthographicSize = maxZoom; // Set the zoom level to maxZoom

        // Initialize the slider if assigned
        if (zoomSlider != null)
        {
            zoomSlider.minValue = minZoom;
            zoomSlider.maxValue = maxZoom;

            // Set the base value of the slider to maxZoom (camera starts zoomed out)
            zoomSlider.value = maxZoom;

            // Add listener for slider changes
            zoomSlider.onValueChanged.AddListener(OnZoomSliderChanged);
        }
    }

    void Update()
    {
        if (!IsPointerOverUI())
        {
            HandleDragging();
            HandleZoom();
        }
    }

    void HandleDragging()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0)) // Left mouse button held
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);

            // Calculate the movement and apply the dragging speed
            Vector3 move = new Vector3(difference.x * dragSpeed, difference.y * dragSpeed, 0); // Lock Z to 0
            transform.Translate(move, Space.World);

            // Apply fixed boundary for camera movement
            Vector3 cameraPos = Camera.main.transform.position;

            // Boundary calculations (Fixed boundary around the camera's position)
            float minX = -boundaryOffset;
            float maxX = boundaryOffset;
            float minY = -boundaryOffset;
            float maxY = boundaryOffset;

            // Apply the camera clamping with the fixed boundaries
            cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
            cameraPos.y = Mathf.Clamp(cameraPos.y, minY, maxY);

            // Update the camera position
            Camera.main.transform.position = cameraPos;

            dragOrigin = Input.mousePosition;
        }
    }

    void OnZoomSliderChanged(float zoomValue)
    {
        // Clamp zoom value to ensure it doesn't go beyond min/max limits
        Camera.main.orthographicSize = Mathf.Clamp(zoomValue, minZoom, maxZoom);
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Adjust zoom value based on scroll input
        if (scrollInput != 0)
        {
            float newZoom = Camera.main.orthographicSize - scrollInput * 600; // Increase multiplier for faster zoom
            Camera.main.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);

            // If there's a slider, update it to match the camera zoom
            if (zoomSlider != null)
            {
                zoomSlider.value = Camera.main.orthographicSize;
            }
        }
    }

    // Check if the pointer is over any UI element
    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
