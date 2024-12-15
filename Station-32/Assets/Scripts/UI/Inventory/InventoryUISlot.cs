using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    private RawImage _rawImage;

    private RawImage _frameImage; 

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();

        CreateFrame();
        SetDefaultFrameTexture();
    }

    private void CreateFrame()
    {
        GameObject frame = new() { name = "Frame" };

        frame.transform.parent = transform;

        frame.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        frame.transform.localScale = Vector3.one;

        _frameImage = frame.AddComponent<RawImage>();
    }

    public void SetFrameTexture(Texture2D texture)
    {
        _frameImage.texture = texture;
    }

    public void SetDefaultFrameTexture()
    {
        Texture2D texture = new(1, 1);

        Color color = new(0.5f, 0.5f, 0.5f, 0.3f);

        texture.SetPixel(0, 0, color);

        texture.Apply();

        _frameImage.texture = texture;
    }

    public void ChangeIcon(Texture image)
    {
        _rawImage.texture = image;
    }
}
