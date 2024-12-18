using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject pinPrefab; // Assign your 3D pin prefab here
    public Transform mapTransform; // Reference the map's Transform
    public float mapScale = 1f; // Adjust to match your map scale

    // Example pin data: replace with your own data or load from JSON/ScriptableObject
    [System.Serializable]
    public struct PinData
    {
        public string description;
        public Sprite image;
        public float latitude; // Latitude in degrees
        public float longitude; // Longitude in degrees
    }

    public PinData[] pins;

    void Start()
    {
        foreach (var pin in pins)
        {
            AddPin(pin);
        }
    }

    void AddPin(PinData pinData)
    {
        // Convert lat/lon to Unity world coordinates
        Vector3 position = LatLonToWorld(pinData.latitude, pinData.longitude);

        // Instantiate pin at the position
        GameObject pin = Instantiate(pinPrefab, mapTransform);
        pin.transform.localPosition = position;

        // Assign popup data
        Pin pinScript = pin.GetComponent<Pin>();
        pinScript.pinImage = pinData.image;
    }

    // Converts latitude and longitude to Unity world coordinates
    private Vector3 LatLonToWorld(float lat, float lon)
    {
        // Adjust these based on your map's origin, scale, and orientation
        float x = (lon - 19.2933f) * mapScale; // Adjust 19.2933f to your map's center longitude
        float z = (lat - 48.0790f) * mapScale; // Adjust 48.0790f to your map's center latitude
        float y = 0f; // Pins are placed at ground level

        return new Vector3(x, y, z);
    }
}
