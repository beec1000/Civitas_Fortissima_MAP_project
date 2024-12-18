using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Image popupImage; // Assign the Image component
    public Button closeButton; // Assign the Close button
    public Canvas canvas;

    public void Setup(Sprite image)
    {
        popupImage.sprite = image;
    }
    void OnMouseDown()
    {
        if (canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
