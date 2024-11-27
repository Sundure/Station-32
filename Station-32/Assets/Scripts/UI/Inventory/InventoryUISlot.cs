using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    private RawImage _icon;
    [SerializeField] private GameObject _frame;

    private void Start()
    {
        _icon = GetComponent<RawImage>();
    }

    public void ChangeIcon(RawImage image)
    {
        _icon.texture = image.texture;
    }

    public void SwitchFrame(bool enabled)
    {
        _frame.SetActive(enabled);
    }
}
