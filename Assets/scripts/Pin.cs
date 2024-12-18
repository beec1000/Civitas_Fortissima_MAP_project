using UnityEngine;
using UnityEngine.UI;

public class Pin : MonoBehaviour
{
    public GameObject popupPrefab; // Assign your popup prefab
    public Canvas canvas; // Assign the UI canvas
    public Sprite pinImage; // Image for this pin
    public Button closeButton; // Reference to the close button

    private GameObject popupInstance;

    void Start()
    {
        // Ensure closeButton has an event listener when the script starts
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseCanvas); // Add the CloseCanvas method as a listener to the button
        }
    }

    void OnMouseDown() // Detect mouse clicks
    {
        if (!canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(true);
        }

        if (popupPrefab == null)
        {
            // Instantiate popup
            popupInstance = Instantiate(popupPrefab, canvas.transform);

            // Optionally set up popup content (uncomment and adjust if needed)
            // popupInstance.GetComponent<Popup>().Setup(pinImage);
        }
    }

    // Method to close the canvas
    void CloseCanvas()
    {
        if (canvas != null && canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(false);
        }

        // Optionally destroy the popup if you want to remove it after closing
        if (popupInstance != null)
        {
            Destroy(popupInstance);
        }
    }
}
